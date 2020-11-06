using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;
using StreamDeck.NET.Message.Received;
using StreamDeck.NET.Message.Sent;

namespace StreamDeck.NET.Client
{
    internal interface IStreamDeckEventHandlerFactory
    {
        IStreamDeckAction CreateAction(string actionUUID, IStreamDeckClient client);
        IEnumerable<IStreamDeckGlobalEvent> CreateGlobalEventHandlers(IStreamDeckClient client);
        IEnumerable<IStreamDeckEventMonitor> CreateEventMonitors(IStreamDeckClient client);
    }

    internal delegate Task StreamDeckDispatchActionMessageFunc(IStreamDeckAction action, IStreamDeckEventMessage message);

    internal delegate Task StreamDeckDispatchTypedActionMessageFunc<in T>(IStreamDeckAction action, T message) where T : IStreamDeckEventMessage;

    internal delegate Task StreamDeckDispatchGlobalEventMessageFunc(IStreamDeckGlobalEvent globalEvent, IStreamDeckEventMessage message);

    internal delegate Task StreamDeckDispatchTypedGlobalEventMessageFunc<in T>(IStreamDeckGlobalEvent globalEvent, T message) where T : IStreamDeckEventMessage;


    internal class StreamDeckClient : IDisposable
    {
        private const int BufferSize = 8192;


        private readonly IStreamDeckLogger logger;
        private readonly IStreamDeckEventHandlerFactory eventHandlerFactory;
        private readonly int port;
        private readonly string pluginUUID;
        private readonly string registerEvent;
        private readonly StreamDeckInfo info;

        private CancellationToken cancellationToken;
        private ClientWebSocket socket;
        private readonly SemaphoreSlim socketSendQueueLock = new SemaphoreSlim(1);

        private readonly Dictionary<ESDSDKEventType, EventTypeInfo> eventTypeInfo = new Dictionary<ESDSDKEventType, EventTypeInfo>();
        private readonly Dictionary<ActionInstanceKey, IStreamDeckAction> actionInstances = new Dictionary<ActionInstanceKey, IStreamDeckAction>();
        

        public StreamDeckClient(IStreamDeckLogger logger, IStreamDeckEventHandlerFactory eventHandlerFactory, int port, string pluginUUID, string registerEvent, StreamDeckInfo info)
        {
            this.logger = logger;
            this.eventHandlerFactory = eventHandlerFactory;
            this.port = port;
            this.pluginUUID = pluginUUID;
            this.registerEvent = registerEvent;
            this.info = info;


            RegisterActionEvent<StreamDeckKeyDownEventMessage>(ESDSDKEventType.keyDown, (action, message) => action.KeyDown(message));
            RegisterActionEvent<StreamDeckKeyUpEventMessage>(ESDSDKEventType.keyUp, (action, message) => action.KeyUp(message));
            RegisterActionEvent<StreamDeckWillAppearEventEventMessage>(ESDSDKEventType.willAppear, (action, message) => action.WillAppear(message));
            RegisterActionEvent<StreamDeckWillDisappearEventEventMessage>(ESDSDKEventType.willDisappear, async (action, message) =>
            {
                await action.WillDisappear(message);
                RemoveActionInstance(message.Action, message.Context);
            });
            RegisterActionEvent<StreamDeckTitleParametersDidChangeEventMessage>(ESDSDKEventType.titleParametersDidChange, (action, message) => action.TitleParametersDidChange(message));
            RegisterActionEvent<StreamDeckDidReceiveSettingsEventMessage>(ESDSDKEventType.didReceiveSettings, (action, message) => action.DidReceiveSettings(message));
            RegisterActionEvent<StreamDeckPropertyInspectorDidAppearEventMessage>(ESDSDKEventType.propertyInspectorDidAppear, (action, message) => action.PropertyInspectorDidAppear(message));
            RegisterActionEvent<StreamDeckPropertyInspectorDidDisappearEventMessage>(ESDSDKEventType.propertyInspectorDidDisappear, (action, message) => action.PropertyInspectorDidDisappear(message));

            RegisterGlobalEvent<StreamDeckDeviceDidConnectEventMessage>(ESDSDKEventType.deviceDidConnect, (globalEvent, message) => globalEvent.DeviceDidConnect(message));
            RegisterGlobalEvent<StreamDeckDeviceDidDisconnectEventMessage>(ESDSDKEventType.deviceDidDisconnect, (globalEvent, message) => globalEvent.DeviceDidDisconnect(message));
            RegisterGlobalEvent<StreamDeckApplicationDidLaunchEventMessage>(ESDSDKEventType.applicationDidLaunch, (globalEvent, message) => globalEvent.ApplicationDidLaunch(message));
            RegisterGlobalEvent<StreamDeckApplicationDidTerminateEventMessage>(ESDSDKEventType.applicationDidTerminate, (globalEvent, message) => globalEvent.ApplicationDidTerminate(message));
            RegisterGlobalEvent<StreamDeckSystemDidWakeUpEventMessage>(ESDSDKEventType.systemDidWakeUp, (globalEvent, message) => globalEvent.SystemDidWakeUp(message));
            RegisterGlobalEvent<StreamDeckDidReceiveGlobalSettingsEventMessage>(ESDSDKEventType.didReceiveGlobalSettings, (globalEvent, message) => globalEvent.DidReceiveGlobalSettings(message));
        }


        public void Dispose()
        {
            foreach (var action in actionInstances.Values)
                action.Dispose();

            actionInstances.Clear();

            socket?.Dispose();
            socketSendQueueLock?.Dispose();
        }


        // ReSharper disable once ParameterHidesMember
        public async Task Run(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;

            var connectInfo = new StreamDeckConnectInfo
            {
                Port = port,
                PluginUUID = pluginUUID,
                RegisterEvent = registerEvent,
                Info = info
            };
            logger.Connecting(connectInfo);

            socket = new ClientWebSocket();
            var uri = new Uri($"ws://localhost:{port}");

            try
            {
                await socket.ConnectAsync(uri, cancellationToken);
                logger.ConnectSuccess(connectInfo);

                await SendMessage(new StreamDeckRegisterMessage
                {
                    Event = registerEvent,
                    UUID = pluginUUID
                });
            }
            catch (Exception e)
            {
                logger.ConnectFailed(connectInfo, e);
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var rawMessage = await ReceiveMessage();
                    if (rawMessage == null)
                        continue;

                    logger.MessageReceived(rawMessage);
                    var message = ParseMessage(rawMessage);
                    if (message == null)
                        continue;

                    DispatchMessage(message, rawMessage);
                }
                catch (Exception)
                {
                    // TODO log error while handling message
                }
            }
        }


        private async Task SendMessage<T>(T message)
        {
            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken);
        }


        private async Task QueueMessage<T>(T message)
        {
            await socketSendQueueLock.WaitAsync(cancellationToken);
            try
            {
                await SendMessage(message);
            }
            finally
            {
                socketSendQueueLock.Release();
            }
        }


        private async Task<string> ReceiveMessage()
        {
            var buffer = new byte[BufferSize];
            using (var messageData = new MemoryStream())
            {
                while (true)
                {
                    var bufferSegment = new ArraySegment<byte>(buffer);
                    var receiveResult = await socket.ReceiveAsync(bufferSegment, cancellationToken);

                    if (receiveResult.Count > 0)
                        await messageData.WriteAsync(buffer, 0, receiveResult.Count, cancellationToken);

                    if (receiveResult.EndOfMessage)
                        break;
                }

                return Encoding.UTF8.GetString(messageData.ToArray());

            }
        }


        private IStreamDeckEventMessage ParseMessage(string rawMessage)
        {
            var message = JObject.Parse(rawMessage);

            var messageEvent = message[StreamDeckConsts.kESDSDKCommonEvent]?.Value<string>();
            if (messageEvent == null)
                return null;

            if (!Enum.TryParse<ESDSDKEventType>(messageEvent, true, out var eventType))
                return null;

            var eventMessageType = CreateEventMessage(eventType);
            if (eventMessageType == null)
                return null;

            return (IStreamDeckEventMessage)message.ToObject(eventMessageType);
        }


        private void DispatchMessage(IStreamDeckEventMessage message, string rawMessage)
        {
            if (!Enum.TryParse<ESDSDKEventType>(message.Event, false, out var eventType))
            {
                logger.MessageUnknownEventType(message, rawMessage);
                return;
            }

            if (!eventTypeInfo.TryGetValue(eventType, out var eventInfo))
            {
                logger.MessageNoEventHandler(message, eventType);
                return;
            }

            if (eventInfo.DispatchActionMessage != null)
            {
                var actionUUID = ((IStreamDeckActionMessage)message).Action;
                var context = ((IStreamDeckContextMessage)message).Context;

                var actions = GetActionInstances(actionUUID, context, eventType != ESDSDKEventType.willDisappear);
                if (actions == null)
                    return;

                foreach (var action in actions)
                    Task.Run(async () => await eventInfo.DispatchActionMessage(action, message), cancellationToken);
            }
            else if (eventInfo.DispatchGlobalEventMessage != null)
            {
                foreach (var globalEvent in GetGlobalEvents())
                    Task.Run(async () => await eventInfo.DispatchGlobalEventMessage(globalEvent, message), cancellationToken);
            }
        }


        private IEnumerable<IStreamDeckAction> GetActionInstances(string actionUUID, string context, bool allowCreate)
        {
            var client = new StreamDeckClientWrapper(this, pluginUUID, context);
            var instanceKey = new ActionInstanceKey(actionUUID, context);

            if (!actionInstances.TryGetValue(instanceKey, out var instance))
            {
                if (allowCreate)
                {
                    instance = eventHandlerFactory.CreateAction(actionUUID, client);
                    actionInstances.Add(instanceKey, instance);
                    yield return instance;
                }
            }
            else
                yield return instance;


            foreach (var monitor in eventHandlerFactory.CreateEventMonitors(client))
                yield return monitor;
        }



        private void RemoveActionInstance(string actionUUID, string context)
        {
            var instanceKey = new ActionInstanceKey(actionUUID, context);
            if (!actionInstances.TryGetValue(instanceKey, out var action)) 
                return;

            action.Dispose();
            actionInstances.Remove(instanceKey);
        }


        private IEnumerable<IStreamDeckGlobalEvent> GetGlobalEvents()
        {
            var client = new StreamDeckClientWrapper(this, pluginUUID, null);

            foreach (var globalEvent in eventHandlerFactory.CreateGlobalEventHandlers(client))
                yield return globalEvent;

            foreach (var monitor in eventHandlerFactory.CreateEventMonitors(client))
                yield return monitor;
        }


        private Type CreateEventMessage(ESDSDKEventType eventType)
        {
            return eventTypeInfo.TryGetValue(eventType, out var eventInfo) ? eventInfo.MessageType : null;
        }


        private void RegisterActionEvent<T>(ESDSDKEventType eventType, StreamDeckDispatchTypedActionMessageFunc<T> dispatchMessage) where T : IStreamDeckEventMessage
        {
            eventTypeInfo.Add(eventType, new EventTypeInfo
            {
                MessageType = typeof(T),
                DispatchActionMessage = (action, message) => dispatchMessage(action, (T)message)
            });
        }


        private void RegisterGlobalEvent<T>(ESDSDKEventType eventType, StreamDeckDispatchTypedGlobalEventMessageFunc<T> dispatchMessage) where T : IStreamDeckEventMessage
        {
            eventTypeInfo.Add(eventType, new EventTypeInfo
            {
                MessageType = typeof(T),
                DispatchGlobalEventMessage = (globalEvent, message) => dispatchMessage(globalEvent, (T)message)
            });
        }


        private class EventTypeInfo
        {
            public Type MessageType;
            public StreamDeckDispatchActionMessageFunc DispatchActionMessage;
            public StreamDeckDispatchGlobalEventMessageFunc DispatchGlobalEventMessage;
        }


        private class ActionInstanceKey
        {
            private readonly string actionUUID;
            private readonly string context;


            public ActionInstanceKey(string actionUUID, string context)
            {
                this.actionUUID = actionUUID;
                this.context = context;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is ActionInstanceKey other))
                    return false;


                return actionUUID == other.actionUUID && context == other.context;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((actionUUID != null ? actionUUID.GetHashCode() : 0) * 397) ^ (context != null ? context.GetHashCode() : 0);
                }
            }
        }


        private class StreamDeckClientWrapper : IStreamDeckClient
        {
            private readonly StreamDeckClient client;

            public string ActionContext { get; private set; }
            public string PluginUUID { get; private set; }


            public StreamDeckClientWrapper(StreamDeckClient client, string pluginUUID,  string actionContext)
            {
                this.client = client;
                PluginUUID = pluginUUID;
                ActionContext = actionContext;
            }

            public async Task SetSettings(JObject payload)
            {
                await client.QueueMessage(new StreamDeckSetSettingsMessage
                {
                    Context = GetContext(),
                    Event = StreamDeckConsts.kESDSDKEventSetSettings,
                    Payload = payload
                });
            }

            public async Task GetSettings()
            {
                await client.QueueMessage(new StreamDeckGetSettingsMessage
                {
                    Context = GetContext(),
                    Event = StreamDeckConsts.kESDSDKEventGetSettings
                });
            }

            public async Task SetGlobalSettings(JObject payload)
            {
                await client.QueueMessage(new StreamDeckSetGlobalSettingsMessage
                {
                    Context = PluginUUID,
                    Event = StreamDeckConsts.kESDSDKEventSetGlobalSettings,
                    Payload = payload
                });
            }

            public async Task GetGlobalSettings()
            {
                await client.QueueMessage(new StreamDeckGetGlobalSettingsMessage
                {
                    Context = PluginUUID,
                    Event = StreamDeckConsts.kESDSDKEventGetGlobalSettings
                });
            }

            public async Task OpenUrl(string url)
            {
                await client.QueueMessage(new StreamDeckOpenUrlMessage
                {
                    Event = StreamDeckConsts.kESDSDKEventOpenURL,
                    Payload = new StreamDeckUrlPayload
                    {
                        Url = url
                    }
                });
            }

            public async Task LogMessage(string message)
            {
                await client.QueueMessage(new StreamDeckLogMessageMessage
                {
                    Event = StreamDeckConsts.kESDSDKEventLogMessage,
                    Payload = new StreamDeckLogMessagePayload
                    {
                        Message = message
                    }
                });
            }

            public async Task SetTitle(string title, ESDSDKTarget target, int? state)
            {
                await client.QueueMessage(new StreamDeckSetTitleMessage
                {
                    Event = StreamDeckConsts.kESDSDKEventSetTitle,
                    Context = GetContext(),
                    Payload = new StreamDeckSetTitlePayload
                    {
                        Title = title,
                        Target = target,
                        State = state
                    }
                });
            }

            public async Task SetImage(string image, ESDSDKTarget target, int? state)
            {
                await client.QueueMessage(new StreamDeckSetImageMessage
                {
                    Event = StreamDeckConsts.kESDSDKEventSetTitle,
                    Context = GetContext(),
                    Payload = new StreamDeckSetImagePayload
                    {
                        Title = image,
                        Target = target,
                        State = state
                    }
                });
            }

            public async Task ShowAlert()
            {
                await client.QueueMessage(new StreamDeckShowAlertMessage
                {
                    Event = StreamDeckConsts.kESDSDKEventShowAlert,
                    Context = GetContext()
                });
            }

            public async Task ShowOk()
            {
                await client.QueueMessage(new StreamDeckShowOkMessage
                {
                    Event = StreamDeckConsts.kESDSDKEventShowOK,
                    Context = GetContext()
                });
            }

            public async Task SetState(int state)
            {
                await client.QueueMessage(new StreamDeckSetStateMessage
                {
                    Event = StreamDeckConsts.kESDSDKEventSetState,
                    Context = GetContext(),
                    Payload = new StreamDeckSetStatePayload
                    {
                        State = state
                    }
                });
            }

            public async Task SwitchToProfile(string device, string profileName)
            {
                await client.QueueMessage(new StreamDeckSwitchToProfileMessage
                {
                    Event = StreamDeckConsts.kESDSDKEventSwitchToProfile,
                    Device = device,
                    Context = GetContext()
                });
            }

            public async Task SendToPropertyInspector(string action, JObject payload)
            {
                await client.QueueMessage(new StreamDeckSendToPropertyInspectorMessage
                {
                    Event = StreamDeckConsts.kESDSDKEventSwitchToProfile,
                    Context = GetContext(),
                    Action = action,
                    Payload = payload
                });
            }

            private string GetContext()
            {
                if (ActionContext == null)
                    throw new InvalidOperationException("This event can only be called from an action with a context");

                return ActionContext;
            }
        }
    }
}

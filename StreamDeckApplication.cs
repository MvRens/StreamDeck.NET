using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using StreamDeck.NET.Attribute;
using StreamDeck.NET.Client;
using StreamDeck.NET.Logger;

namespace StreamDeck.NET
{
    /// <summary>
    /// Thrown when there is an error in the configuration.
    /// </summary>
    public class StreamDeckConfigurationError : Exception
    {
        /// <inheritdoc />
        public StreamDeckConfigurationError(string message) : base(message) { }
    }


    /// <summary>
    /// Thrown when a command-line parameter is missing. Indicates a manual run or a change in the Stream Deck SDK.
    /// </summary>
    public class StreamDeckMissingParameter : Exception
    {
        /// <inheritdoc />
        public StreamDeckMissingParameter(string message) : base(message) { }
    }


    /// <summary>
    /// Thrown when a command-line parameter is malformed. Indicates a manual run or a change in the Stream Deck SDK.
    /// </summary>
    public class StreamDeckInvalidParameter : Exception
    {
        /// <inheritdoc />
        public StreamDeckInvalidParameter(string message) : base(message) { }
    }


    /// <summary>
    /// Factory method for creating a IStreamDeckAction instance.
    /// </summary>
    /// <param name="client">The client for the action instance.</param>
    public delegate IStreamDeckAction StreamDeckActionFactory(IStreamDeckClient client);


    /// <summary>
    /// Factory method for creating a IStreamDeckGlobalEvent instance.
    /// </summary>
    /// <param name="client">The client for the global event.</param>
    public delegate IStreamDeckGlobalEvent StreamDeckGlobalEventFactory(IStreamDeckClient client);


    /// <summary>
    /// Factory method for creating a IStreamDeckEventMonitor instance.
    /// </summary>
    /// <param name="client">The client for the event.</param>
    public delegate IStreamDeckEventMonitor StreamDeckEventMonitorFactory(IStreamDeckClient client);


    /// <summary>
    /// Wrapped class for the Stream Deck SDK. Configure as desired in your programs' main()
    /// and call at least one of the Register methods to add actions. Finally, call Run with the args as passed
    /// to main() to initiate the registration process.
    /// </summary>
    [PublicAPI]
    public class StreamDeckApplication
    {
        private readonly object factoriesLock = new object();
        private readonly Dictionary<string, StreamDeckActionFactory> actionFactories = new Dictionary<string, StreamDeckActionFactory>();
        private readonly List<StreamDeckGlobalEventFactory> globalEventFactories = new List<StreamDeckGlobalEventFactory>();
        private readonly List<StreamDeckEventMonitorFactory> eventMonitorFactories = new List<StreamDeckEventMonitorFactory>();
        private IStreamDeckLogger logger;


        /// <summary>
        /// Use Instance instead.
        /// </summary>
        private StreamDeckApplication()
        {
        }


        /// <summary>
        /// Returns the singleton instance of the StreamDeckApplication.
        /// </summary>
        public static StreamDeckApplication Instance => new StreamDeckApplication();


        /// <summary>
        /// Registers all actions marked with the StreamDeckAction attribute, and all global event handlers
        /// marked with the StreamDeckGlobalEvent attribute, in the entry assembly.
        ///
        /// Classes registered this way must have either a parameterless constructor, or a constructor
        /// with a single IStreamDeckClient parameter.
        /// </summary>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterAll()
        {
            return RegisterAll(Assembly.GetEntryAssembly());
        }


        /// <summary>
        /// Registers all actions marked with the StreamDeckAction attribute, and all global event handlers
        /// marked with the StreamDeckGlobalEvent attribute, in the specified assembly.
        ///
        /// Classes registered this way must have either a parameterless constructor, or a constructor
        /// with a single IStreamDeckClient parameter.
        /// </summary>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterAll(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(t => t.IsDefined(typeof(StreamDeckActionAttribute))))
                RegisterAction(type);

            foreach (var type in assembly.GetTypes().Where(t => t.IsDefined(typeof(StreamDeckGlobalEventAttribute))))
                RegisterGlobalEvent(type);

            return this;
        }


        /// <summary>
        /// Registers an action. The specified type must implement IStreamDeckAction and have a StreamDeckAction attribute.
        ///
        /// Classes registered this way must have either a parameterless constructor, or a constructor
        /// with a single IStreamDeckClient parameter.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterAction<T>() where T : class
        {
            return RegisterAction(typeof(T));
        }


        /// <summary>
        /// Registers an action. The specified type must implement IStreamDeckAction and have a StreamDeckAction attribute.
        ///
        /// Classes registered this way must have either a parameterless constructor, or a constructor
        /// with a single IStreamDeckClient parameter.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterAction(Type type)
        {
            return RegisterAction(type, CreateActionFactory(type));
        }


        /// <summary>
        /// Registers an action. The specified type must implement IStreamDeckAction and have a StreamDeckAction attribute. 
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <param name="factory">The factory method to be called when an action instance is created.</param>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterAction(Type type, StreamDeckActionFactory factory)
        {
            if (!typeof(IStreamDeckAction).IsAssignableFrom(type))
                throw new StreamDeckConfigurationError($"Action type {type.FullName} must implement IStreamDeckAction");

            var actionAttribute = type.GetCustomAttribute<StreamDeckActionAttribute>();
            if (actionAttribute == null)
                throw new StreamDeckConfigurationError($"Action type {type.FullName} must be annotated with the StreamDeckAction attribute");

            lock (factoriesLock)
            {
                actionFactories.Add(actionAttribute.UUID, factory);
            }

            return this;
        }


        /// <summary>
        /// Registers an global event handler. The specified type must implement IStreamDeckGlobalEvent.
        ///
        /// Classes registered this way must have either a parameterless constructor, or a constructor
        /// with a single IStreamDeckClient parameter.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterGlobalEvent<T>() where T : class
        {
            return RegisterGlobalEvent(typeof(T));
        }


        /// <summary>
        /// Registers an global event handler. The specified type must implement IStreamDeckGlobalEvent.
        ///
        /// Classes registered this way must have either a parameterless constructor, or a constructor
        /// with a single IStreamDeckClient parameter.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterGlobalEvent(Type type)
        {
            return RegisterGlobalEvent(type, CreateGlobalEventFactory(type));
        }


        /// <summary>
        /// Registers a global event handler. The specified type must implement IStreamDeckGlobalEvent.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <param name="factory">The factory method to be called when a global event handler instance is created.</param>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterGlobalEvent(Type type, StreamDeckGlobalEventFactory factory)
        {
            if (!typeof(IStreamDeckGlobalEvent).IsAssignableFrom(type))
                throw new StreamDeckConfigurationError($"Action type {type.FullName} must implement IStreamDeckGlobalEvent");

            lock (factoriesLock)
            {
                globalEventFactories.Add(factory);
            }

            return this;
        }


        /// <summary>
        /// Registers an event monitor. The specified type must implement IStreamDeckEventMonitor.
        ///
        /// Classes registered this way must have either a parameterless constructor, or a constructor
        /// with a single IStreamDeckClient parameter.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterEventMonitor<T>() where T : class
        {
            return RegisterEventMonitor(typeof(T));
        }

        /// <summary>
        /// Registers an event monitor. The specified type must implement IStreamDeckEventMonitor.
        ///
        /// Classes registered this way must have either a parameterless constructor, or a constructor
        /// with a single IStreamDeckClient parameter.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterEventMonitor(Type type)
        {
            return RegisterEventMonitor(type, CreateEventMonitorFactory(type));
        }


        /// <summary>
        /// Registers a event monitor. The specified type must implement IStreamDeckEventMonitor.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <param name="factory">The factory method to be called when an event monitor instance is created.</param>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication RegisterEventMonitor(Type type, StreamDeckEventMonitorFactory factory)
        {
            if (!typeof(IStreamDeckEventMonitor).IsAssignableFrom(type))
                throw new StreamDeckConfigurationError($"Action type {type.FullName} must implement IStreamDeckEventMonitor");

            lock (eventMonitorFactories)
            {
                eventMonitorFactories.Add(factory);
            }

            return this;
        }


        /// <summary>
        /// Sets the logger implementation to use.
        /// </summary>
        /// <param name="newLogger">The logger implementation.</param>
        /// <returns>The StreamDeckApplication instance for method chaining purposes.</returns>
        public StreamDeckApplication WithLogger(IStreamDeckLogger newLogger)
        {
            logger = newLogger;
            return this;
        }


        /// <summary>
        /// Processes the parameters passed by StreamDeck and starts the registration process.
        /// </summary>
        /// <param name="args">The arguments as passed to the main() method.</param>
        public void Run(string[] args)
        {
            var portString = GetArgument(args, StreamDeckConsts.kESDSDKPortParameter);
            var pluginUUID = GetArgument(args, StreamDeckConsts.kESDSDKPluginUUIDParameter);
            var registerEvent = GetArgument(args, StreamDeckConsts.kESDSDKRegisterEventParameter);
            var infoString = GetArgument(args, StreamDeckConsts.kESDSDKInfoParameter);

            if (portString == null) throw new StreamDeckMissingParameter($"{StreamDeckConsts.kESDSDKPortParameter} argument missing");
            if (pluginUUID == null) throw new StreamDeckMissingParameter($"{StreamDeckConsts.kESDSDKPluginUUIDParameter} argument missing");
            if (registerEvent == null) throw new StreamDeckMissingParameter($"{StreamDeckConsts.kESDSDKRegisterEventParameter} argument missing");
            if (infoString == null) throw new StreamDeckMissingParameter($"{StreamDeckConsts.kESDSDKInfoParameter} argument missing");

            if (!int.TryParse(portString, out var port) || port < 0 || port > 65535)
                throw new StreamDeckInvalidParameter("-port argument must be an integer between 0 and 65536");

            var info = JsonConvert.DeserializeObject<StreamDeckInfo>(infoString);


            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;

                Console.WriteLine("Closing connection...");
                cancellationTokenSource.Cancel();
            };


            var loggerInstance = logger ?? 
                (Debugger.IsAttached 
                    ? (IStreamDeckLogger)new DebugLogger() 
                    : new DevNullLogger());

            using (var worker = new StreamDeckClient(loggerInstance, new StreamDeckEventHandlerFactory(this), port, pluginUUID, registerEvent, info))
            {
                // ReSharper disable MethodSupportsCancellation - handled by StreamDeckWorker
                // ReSharper disable once AccessToDisposedClosure - not an issue, task is awaited
                var workerTask = Task.Run(async () => { await worker.Run(cancellationTokenSource.Token); });

                try
                {
                    workerTask.Wait();
                }
                catch (AggregateException e)
                {
                    if (e.InnerExceptions.Count != 1 || !(e.InnerExceptions[0] is TaskCanceledException))
                        throw;
                }
                catch (TaskCanceledException)
                {
                    // This space intentionally left blank
                }
                // ReSharper restore MethodSupportsCancellation
            }
        }


        private static string GetArgument(IReadOnlyList<string> args, string key)
        {
            for (var argIndex = 0; argIndex < args.Count - 1; argIndex++)
            {
                if (string.Equals(args[argIndex], key, StringComparison.InvariantCultureIgnoreCase))
                    return args[argIndex + 1];
            }

            return null;
        }


        private static StreamDeckActionFactory CreateActionFactory(Type type)
        {
            return context => (IStreamDeckAction)CreateTypeFactory(type)(context);
        }


        private static StreamDeckGlobalEventFactory CreateGlobalEventFactory(Type type)
        {
            return context => (IStreamDeckGlobalEvent)CreateTypeFactory(type)(context);
        }


        private static StreamDeckEventMonitorFactory CreateEventMonitorFactory(Type type)
        {
            return context => (IStreamDeckEventMonitor)CreateTypeFactory(type)(context);
        }


        private static Func<IStreamDeckClient, object> CreateTypeFactory(Type type)
        {
            var constructorWithContext = type.GetConstructor(new[] { typeof(IStreamDeckClient) });
            if (constructorWithContext != null)
                return context => (IStreamDeckAction)constructorWithContext.Invoke(new object[] { context });


            var constructorWithoutParameters = type.GetConstructor(new Type[0]);
            if (constructorWithoutParameters != null)
                return context => (IStreamDeckAction)constructorWithoutParameters.Invoke(new object[0]);

            throw new StreamDeckConfigurationError($"Action type {type.FullName} must have a constructor with an IStreamDeckClient parameter or a parameterless constructor");
        }


        private class StreamDeckEventHandlerFactory : IStreamDeckEventHandlerFactory
        {
            private readonly StreamDeckApplication application;

            public StreamDeckEventHandlerFactory(StreamDeckApplication application)
            {
                this.application = application;
            }

            public IStreamDeckAction CreateAction(string actionUUID, IStreamDeckClient client)
            {
                lock (application.factoriesLock)
                {
                    return application.actionFactories.TryGetValue(actionUUID, out var factory)
                        ? factory(client)
                        : null;
                }
            }

            public IEnumerable<IStreamDeckGlobalEvent> CreateGlobalEventHandlers(IStreamDeckClient client)
            {
                lock (application.factoriesLock)
                {
                    return application.globalEventFactories.Select(f => f(client)).ToList();
                }
            }

            public IEnumerable<IStreamDeckEventMonitor> CreateEventMonitors(IStreamDeckClient client)
            {
                lock (application.eventMonitorFactories)
                {
                    return application.eventMonitorFactories.Select(f => f(client)).ToList();
                }
            }
        }
    }
}

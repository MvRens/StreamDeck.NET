using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using StreamDeck.NET.Message.Received;

namespace StreamDeck.NET
{
    /// <summary>
    /// Provides a convenient wrapper for request-response style messages. Allows the calling code
    /// to simply await the call in a procedural fashion.
    /// </summary>
    [PublicAPI]
    public static class StreamDeckRequest
    {
        private static bool isRegistered;
        private static readonly object ResponseLock = new object();
        private static readonly Dictionary<string, TaskCompletionSource<JObject>> SettingsResponse = new Dictionary<string, TaskCompletionSource<JObject>>();
        private static TaskCompletionSource<JObject> globalSettingsResponse;

        /// <summary>
        /// Wraps the getSettings and didReceiveSettings events into a single procedural method.
        /// </summary>
        /// <param name="client">An IStreamDeckClient instance as passed to the constructor of an IStreamDeckAction.</param>
        /// <remarks>
        /// Not available from a global event.
        /// </remarks>
        /// <returns>A json object containing data that you can set and are stored persistently.</returns>
        public static async Task<JObject> GetSettings(IStreamDeckClient client)
        {
            TaskCompletionSource<JObject> completionSource;
            var sendRequest = false;

            lock (ResponseLock)
            {
                CheckRegistered();

                // If a request is already running, await the same response
                if (!SettingsResponse.TryGetValue(client.ActionContext, out completionSource))
                {
                    completionSource = new TaskCompletionSource<JObject>();
                    SettingsResponse.Add(client.ActionContext, completionSource);

                    sendRequest = true;
                }
            }

            if (sendRequest)
                await client.GetSettings();

            return await completionSource.Task;
        }


        /// <summary>
        /// Wraps the getGlobalSettings and didReceiveGlobalSettings events into a single procedural method.
        /// </summary>
        /// <param name="client">An IStreamDeckClient instance as passed to the constructor of an IStreamDeckAction or IStreamDeckGlobalEvent.</param>
        /// <returns>A json object containing data that you can set and are stored persistently.</returns>
        public static async Task<JObject> GetGlobalSettings(IStreamDeckClient client)
        {
            TaskCompletionSource<JObject> completionSource;
            var sendRequest = false;

            lock (ResponseLock)
            {
                CheckRegistered();

                // If a request is already running, await the same response
                completionSource = globalSettingsResponse;
                if (completionSource == null)
                {
                    completionSource = new TaskCompletionSource<JObject>();
                    globalSettingsResponse = completionSource;

                    sendRequest = true;
                }
            }

            if (sendRequest)
                await client.GetGlobalSettings();

            return await completionSource.Task;
        }


        internal static void HandleDidReceiveSettings(StreamDeckDidReceiveSettingsEventMessage message)
        {
            TaskCompletionSource<JObject> completionSource;

            lock (ResponseLock)
            {
                if (SettingsResponse.TryGetValue(message.Context, out completionSource))
                    SettingsResponse.Remove(message.Context);
            }

            completionSource?.TrySetResult(message.Payload.Settings);
        }


        internal static void HandleDidReceiveGlobalSettings(StreamDeckDidReceiveGlobalSettingsEventMessage message)
        {
            TaskCompletionSource<JObject> completionSource;

            lock (ResponseLock)
            {
                completionSource = globalSettingsResponse;
                globalSettingsResponse = null;
            }

            completionSource?.TrySetResult(message.Payload.Settings);
        }


        private static void CheckRegistered()
        {
            if (isRegistered) 
                return;

            StreamDeckApplication.Instance.RegisterEventMonitor<StreamDeckRequestEventMonitor>();
            isRegistered = true;
        }
    }


    internal class StreamDeckRequestEventMonitor : StreamDeckBaseEventMonitor
    {
        public override Task DidReceiveSettings(StreamDeckDidReceiveSettingsEventMessage message)
        {
            StreamDeckRequest.HandleDidReceiveSettings(message);
            return Task.CompletedTask;
        }


        public override Task DidReceiveGlobalSettings(StreamDeckDidReceiveGlobalSettingsEventMessage message)
        {
            StreamDeckRequest.HandleDidReceiveGlobalSettings(message);
            return Task.CompletedTask;
        }
    }
}

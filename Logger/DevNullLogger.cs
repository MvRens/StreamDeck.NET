using System;
using JetBrains.Annotations;
using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Logger
{
    /// <summary>
    /// Logs to the void.
    /// </summary>
    [PublicAPI]
    public class DevNullLogger : IStreamDeckLogger
    {
        /// <inheritdoc />
        public void Connecting(StreamDeckConnectInfo info) { }

        /// <inheritdoc />
        public void ConnectFailed(StreamDeckConnectInfo info, Exception exception) { }

        /// <inheritdoc />
        public void ConnectSuccess(StreamDeckConnectInfo info) { }

        /// <inheritdoc />
        public void MessageReceived(string message) { }

        /// <inheritdoc />
        public void MessageUnknownEventType(IStreamDeckEventMessage message, string rawMessage) { }

        /// <inheritdoc />
        public void MessageNoEventHandler(IStreamDeckEventMessage message, ESDSDKEventType eventType) { }
    }
}

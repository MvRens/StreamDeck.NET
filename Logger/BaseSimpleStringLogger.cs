using System;
using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Logger
{
    /// <summary>
    /// Base class for loggers who output a simple log message and do not need structured
    /// or localizable logging.
    /// </summary>
    public abstract class BaseSimpleStringLogger : IStreamDeckLogger
    {
        private readonly bool verbose;

        /// <summary>
        /// Creates a new logger which outputs to the console.
        /// </summary>
        /// <param name="verbose">Determines if verbose messages are also logged.</param>
        protected BaseSimpleStringLogger(bool verbose)
        {
            this.verbose = verbose;
        }


        /// <summary>
        /// Receives a preformatted log message.
        /// </summary>
        protected abstract void Log(string message);


        /// <inheritdoc />
        public void Connecting(StreamDeckConnectInfo info)
        {
            Log($"Connecting to ws://localhost:{info.Port}");
        }

        /// <inheritdoc />
        public void ConnectFailed(StreamDeckConnectInfo info, Exception exception)
        {
            Log($"Failed to connect: {exception.Message}");
        }

        /// <inheritdoc />
        public void ConnectSuccess(StreamDeckConnectInfo info)
        {
            Log("Connected succesfully");
        }

        /// <inheritdoc />
        public void MessageReceived(string message)
        {
            if (!verbose)
                return;

            Log($"Message received: {message}");
        }

        /// <inheritdoc />
        public void MessageUnknownEventType(IStreamDeckEventMessage message, string rawMessage)
        {
            Log($"Message event type is unknown: {message.Event}, raw message: {rawMessage}");
        }

        /// <inheritdoc />
        public void MessageNoEventHandler(IStreamDeckEventMessage message, ESDSDKEventType eventType)
        {
            Log($"Message event type is not yet handled by the StreamDeck.NET library: {eventType}");
        }
    }
}

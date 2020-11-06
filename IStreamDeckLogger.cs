using System;
using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET
{
    /// <summary>
    /// Contains information about the Stream Deck connection for logging purposes.
    /// </summary>
    public class StreamDeckConnectInfo
    {
        /// <summary>
        /// The websocket port as provided by Stream Deck in the command-line parameters.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The plugin UUID as provided by Stream Deck in the command-line parameters.
        /// </summary>
        public string PluginUUID { get; set; }

        /// <summary>
        /// The register event as provided by Stream Deck in the command-line parameters.
        /// </summary>
        public string RegisterEvent { get; set; }

        /// <summary>
        /// The info object as provided by Stream Deck in the command-line parameters.
        /// </summary>
        public StreamDeckInfo Info { get; set; }
    }


    /// <summary>
    /// Provides an abstraction between StreamDeck.NET and a logging framework.
    /// Methods are very specific by design, so that a structured logging framework
    /// like Serilog can be implemented more easily.
    /// </summary>
    public interface IStreamDeckLogger
    {
        /// <summary>
        /// Called when an attempt is made to connect to the Stream Deck websocket.
        /// </summary>
        /// <param name="info">Information about the Stream Deck connection.</param>
        void Connecting(StreamDeckConnectInfo info);

        /// <summary>
        /// Called when the connection to the Stream Deck websocket failed.
        /// </summary>
        /// <param name="info">Information about the Stream Deck connection.</param>
        /// <param name="exception">The exception raised by the websocket.</param>
        void ConnectFailed(StreamDeckConnectInfo info, Exception exception);

        /// <summary>
        /// Called when the connection to the Stream Deck websocket has been succesfully made.
        /// </summary>
        /// <param name="info">Information about the Stream Deck connection.</param>
        void ConnectSuccess(StreamDeckConnectInfo info);

        /// <summary>
        /// Called when a message has been received, decoded but unparsed. Intended for verbose/debug level logging.
        /// </summary>
        /// <param name="message">The message data, UTF-8 decoded.</param>
        void MessageReceived(string message);

        /// <summary>
        /// Callen when a message has been received with an unsupported event type.
        /// </summary>
        /// <param name="message">The message who's Event property contains an unknown value.</param>
        /// <param name="rawMessage">The message data, UTF-8 decoded.</param>
        void MessageUnknownEventType(IStreamDeckEventMessage message, string rawMessage);

        /// <summary>
        /// Called when a message has been received but there is no registered handler for the event type. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventType"></param>
        void MessageNoEventHandler(IStreamDeckEventMessage message, ESDSDKEventType eventType);
    }
}

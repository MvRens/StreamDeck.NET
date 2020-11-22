using Newtonsoft.Json.Linq;
using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when the Property Inspector sends a sendToPlugin event.
    /// </summary>
    public class StreamDeckSendToPluginEventMessage : IStreamDeckActionMessage, IStreamDeckContextMessage, IStreamDeckPayloadMessage<JObject>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Action { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public JObject Payload { get; set; }
    }
}

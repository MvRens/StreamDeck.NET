using Newtonsoft.Json.Linq;
using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to temporarily show an OK checkmark icon on the image displayed by an instance of an action.
    /// </summary>
    public class StreamDeckSendToPropertyInspectorMessage : IStreamDeckActionMessage, IStreamDeckContextMessage, IStreamDeckPayloadMessage<JObject>
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

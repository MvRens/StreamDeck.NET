using Newtonsoft.Json.Linq;
using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to save data persistently for the action's instance.
    /// </summary>
    public class StreamDeckSetSettingsMessage : IStreamDeckContextMessage, IStreamDeckPayloadMessage<JObject>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public JObject Payload { get; set; }
    }
}

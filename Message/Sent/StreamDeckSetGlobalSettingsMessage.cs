using Newtonsoft.Json.Linq;
using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to save data securely and globally for the plugin.
    /// </summary>
    public class StreamDeckSetGlobalSettingsMessage : IStreamDeckContextMessage, IStreamDeckPayloadMessage<JObject>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public JObject Payload { get; set; }
    }
}

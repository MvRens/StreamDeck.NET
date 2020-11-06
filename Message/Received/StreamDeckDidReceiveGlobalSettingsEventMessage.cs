using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received after calling the getGlobalSettings API to retrieve the global persistent data stored for the plugin.
    /// </summary>
    public class StreamDeckDidReceiveGlobalSettingsEventMessage : IStreamDeckPayloadMessage<StreamDeckSettingsPayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public StreamDeckSettingsPayload Payload { get; set; }
    }
}

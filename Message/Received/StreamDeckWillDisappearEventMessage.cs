using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when an instance of an action ceases to be displayed on Stream Deck,
    /// for example when switching profiles or folders
    /// </summary>
    public class StreamDeckWillDisappearEventEventMessage : IStreamDeckActionMessage, IStreamDeckContextMessage, IStreamDeckDeviceMessage, IStreamDeckPayloadMessage<StreamDeckActionSettingsPayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Action { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public string Device { get; set; }

        /// <inheritdoc />
        public StreamDeckActionSettingsPayload Payload { get; set; }
    }
}

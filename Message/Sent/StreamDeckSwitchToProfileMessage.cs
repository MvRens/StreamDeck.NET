using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to switch to one of the preconfigured read-only profiles.
    /// </summary>
    public class StreamDeckSwitchToProfileMessage : IStreamDeckContextMessage, IStreamDeckDeviceMessage, IStreamDeckPayloadMessage<StreamDeckSwitchToProfilePayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public string Device { get; set; }

        /// <inheritdoc />
        public StreamDeckSwitchToProfilePayload Payload { get; set; }
    }
}

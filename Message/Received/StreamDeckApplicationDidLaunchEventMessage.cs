using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when a monitored application is launched.
    /// </summary>
    public class StreamDeckApplicationDidLaunchEventMessage : IStreamDeckPayloadMessage<StreamDeckApplicationPayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public StreamDeckApplicationPayload Payload { get; set; }
    }
}
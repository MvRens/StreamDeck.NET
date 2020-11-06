using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when a monitored application is terminated.
    /// </summary>
    public class StreamDeckApplicationDidTerminateEventMessage : IStreamDeckPayloadMessage<StreamDeckApplicationPayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public StreamDeckApplicationPayload Payload { get; set; }
    }
}

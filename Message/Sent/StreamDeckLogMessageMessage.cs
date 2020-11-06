using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to write a debug log to the logs file.
    /// </summary>
    public class StreamDeckLogMessageMessage : IStreamDeckPayloadMessage<StreamDeckLogMessagePayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public StreamDeckLogMessagePayload Payload { get; set; }
    }
}

using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to open an URL in the default browser.
    /// </summary>
    public class StreamDeckOpenUrlMessage : IStreamDeckPayloadMessage<StreamDeckUrlPayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public StreamDeckUrlPayload Payload { get; set; }
    }
}

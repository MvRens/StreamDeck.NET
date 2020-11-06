using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to dynamically change the image displayed by an instance of an action.
    /// </summary>
    public class StreamDeckSetImageMessage : IStreamDeckContextMessage, IStreamDeckPayloadMessage<StreamDeckSetImagePayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public StreamDeckSetImagePayload Payload { get; set; }
    }
}

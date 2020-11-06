using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to dynamically change the title of an instance of an action.
    /// </summary>
    public class StreamDeckSetTitleMessage : IStreamDeckContextMessage, IStreamDeckPayloadMessage<StreamDeckSetTitlePayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public StreamDeckSetTitlePayload Payload { get; set; }
    }
}

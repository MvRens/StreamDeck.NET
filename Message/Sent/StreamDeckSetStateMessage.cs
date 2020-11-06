using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to change the state of the action's instance supporting multiple states.
    /// </summary>
    public class StreamDeckSetStateMessage : IStreamDeckContextMessage, IStreamDeckPayloadMessage<StreamDeckSetStatePayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public StreamDeckSetStatePayload Payload { get; set; }
    }
}

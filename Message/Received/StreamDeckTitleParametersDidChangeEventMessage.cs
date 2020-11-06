using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when the user changes the title or title parameters of the instance of an action.
    /// </summary>
    public class StreamDeckTitleParametersDidChangeEventMessage : IStreamDeckActionMessage, IStreamDeckContextMessage, IStreamDeckDeviceMessage, IStreamDeckPayloadMessage<StreamDeckTitleActionPayload>
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
        public StreamDeckTitleActionPayload Payload { get; set;  }
    }
}

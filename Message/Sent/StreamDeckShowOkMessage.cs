using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to temporarily show an OK checkmark icon on the image displayed by an instance of an action.
    /// </summary>
    public class StreamDeckShowOkMessage : IStreamDeckContextMessage
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }
    }
}

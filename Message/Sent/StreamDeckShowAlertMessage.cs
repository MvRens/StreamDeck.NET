using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to temporarily show an alert icon on the image displayed by an instance of an action.
    /// </summary>
    public class StreamDeckShowAlertMessage : IStreamDeckContextMessage
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }
    }
}

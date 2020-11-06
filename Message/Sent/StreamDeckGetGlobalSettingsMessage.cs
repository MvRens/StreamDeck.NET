using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to request the global persistent data.
    /// </summary>
    public class StreamDeckGetGlobalSettingsMessage : IStreamDeckContextMessage
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }
    }
}

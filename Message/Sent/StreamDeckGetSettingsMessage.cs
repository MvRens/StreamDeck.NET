using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to request the persistent data for the action's instance.
    /// </summary>
    public class StreamDeckGetSettingsMessage : IStreamDeckContextMessage
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }
    }
}

using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when the computer is wake up.
    /// </summary>
    public class StreamDeckSystemDidWakeUpEventMessage : IStreamDeckEventMessage
    {
        /// <inheritdoc />
        public string Event { get; set;  }
    }
}

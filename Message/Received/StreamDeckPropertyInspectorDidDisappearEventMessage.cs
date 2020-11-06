using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when the Property Inspector disappears.
    /// </summary>
    public class StreamDeckPropertyInspectorDidDisappearEventMessage : IStreamDeckActionMessage, IStreamDeckContextMessage, IStreamDeckDeviceMessage
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Action { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public string Device { get; set; }
    }
}

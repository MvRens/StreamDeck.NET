using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received after calling the getSettings API to retrieve the persistent data stored for the action.
    /// </summary>
    public class StreamDeckPropertyInspectorDidAppearEventMessage : IStreamDeckActionMessage, IStreamDeckContextMessage, IStreamDeckDeviceMessage
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

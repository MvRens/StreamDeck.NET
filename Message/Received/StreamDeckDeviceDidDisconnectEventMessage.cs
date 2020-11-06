using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when a device is unplugged from the computer.
    /// </summary>
    public class StreamDeckDeviceDidDisconnectEventMessage : IStreamDeckDeviceMessage
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Device { get; set; }
    }
}

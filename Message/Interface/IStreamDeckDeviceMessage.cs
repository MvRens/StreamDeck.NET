using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Interface
{
    /// <summary>
    /// Signifies messages that contain a device parameter.
    /// </summary>
    public interface IStreamDeckDeviceMessage : IStreamDeckEventMessage
    {
        /// <summary>
        /// An opaque value identifying the device.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKCommonDevice)]
        string Device { get; set; }
    }
}

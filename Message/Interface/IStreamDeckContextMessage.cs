using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Interface
{
    /// <summary>
    /// Signifies messages that contain a context parameter.
    /// </summary>
    public interface IStreamDeckContextMessage : IStreamDeckEventMessage
    {
        /// <summary>
        /// An opaque value identifying the instance's action. You will need to pass this opaque value to several APIs like the setTitle API.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKCommonContext)]
        string Context { get; set; }
    }
}

using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Interface
{
    /// <summary>
    /// Signifies messages that contain an event parameter.
    /// </summary>
    public interface IStreamDeckEventMessage
    {
        /// <summary>
        /// The type of the received event.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKCommonEvent)]
        string Event { get; set; }
    }
}

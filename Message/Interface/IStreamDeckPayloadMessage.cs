using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Interface
{
    /// <summary>
    /// Signifies messages that contain a payload parameter.
    /// </summary>
    public interface IStreamDeckPayloadMessage<T> : IStreamDeckEventMessage where T : class
    {
        /// <summary>
        /// A json object
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKCommonPayload)]
        T Payload { get; set; }
    }
}

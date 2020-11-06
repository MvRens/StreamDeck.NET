using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payload containing an URL.
    /// </summary>
    public class StreamDeckUrlPayload
    {
        /// <summary>
        /// An URL to open in the default browser.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadURL)]
        public string Url { get; set; }
    }
}
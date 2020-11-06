using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payloading containing settings.
    /// </summary>
    public class StreamDeckSettingsPayload
    {
        /// <summary>
        /// This json object contains data that you can set and are stored persistently.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadSettings)]
        public JObject Settings { get; set; }
    }
}
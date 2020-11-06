using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payload containing a profile.
    /// </summary>
    public class StreamDeckSwitchToProfilePayload
    {
        /// <summary>
        /// The name of the profile to switch to. The name should be identical to the name provided in the manifest.json file.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadProfile)]
        public string Profile { get; set; }
    }
}
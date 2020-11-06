using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payload containing a state.
    /// </summary>
    public class StreamDeckSetStatePayload
    {
        /// <summary>
        /// A 0-based integer value representing the state requested.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadState)]
        public int State { get; set; }
    }
}
using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payload containing application information.
    /// </summary>
    public class StreamDeckApplicationPayload
    {
        /// <summary>
        /// The identifier of the application that has been launched.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadApplication)]
        public string Application { get; set; }
    }
}
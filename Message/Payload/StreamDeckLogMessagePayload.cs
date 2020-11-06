using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payload containing a log message.
    /// </summary>
    public class StreamDeckLogMessagePayload
    {
        /// <summary>
        /// A string to write to the logs file.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadMessage)]
        public string Message { get; set; }
    }
}
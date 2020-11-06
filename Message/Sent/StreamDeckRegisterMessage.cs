using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Sent
{
    /// <summary>
    /// Sent to Stream Deck after the websocket is connected.
    /// </summary>
    public class StreamDeckRegisterMessage
    {
        /// <summary>
        /// The registerEvent parameter as passed to the plugin
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKCommonEvent)]
        public string Event { get; set; }

        /// <summary>
        /// The pluginUUID parameter as passed to the plugin
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKRegisterUUID)]
        public string UUID { get; set; }
    }
}

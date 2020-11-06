using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payload containing a title.
    /// </summary>
    public class StreamDeckSetTitlePayload
    {
        /// <summary>
        /// The title to display. If there is no title parameter, the title is reset to the title set by the user.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadTitle)]
        public string Title { get; set; }

        /// <summary>
        /// Specify if you want to display the title on the hardware and software (0), only on the hardware (1) or only on the software (2). Default is 0.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadTarget)]
        public ESDSDKTarget Target { get; set; }

        /// <summary>
        /// A 0-based integer value representing the state of an action with multiple states. This is an optional parameter. If not specified, the title is set to all states.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadState)]
        public int? State { get; set; }
    }
}
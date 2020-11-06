using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payload containing an image.
    /// </summary>
    public class StreamDeckSetImagePayload
    {
        /// <summary>
        /// The image to display encoded in base64 with the image format declared in the mime type (PNG, JPEG, BMP, ...). svg is also supported. If no image is passed, the image is reset to the default image from the manifest.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadTitle)]
        public string Title { get; set; }

        /// <summary>
        /// 	Specify if you want to display the title on the hardware and software (0), only on the hardware (1) or only on the software (2). Default is 0.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadTarget)]
        public ESDSDKTarget Target { get; set; }

        /// <summary>
        /// A 0-based integer value representing the state of an action with multiple states. This is an optional parameter. If not specified, the image is set to all states.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadState)]
        public int? State { get; set; }
    }
}
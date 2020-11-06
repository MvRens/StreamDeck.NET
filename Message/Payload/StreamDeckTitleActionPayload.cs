using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payload containing title information.
    /// </summary>
    public class StreamDeckTitleActionPayload : StreamDeckActionSettingsPayload
    {
        /// <summary>
        /// The new title.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadTitle)]
        public string Title { get; set; }


        /// <summary>
        /// A json object describing the new title parameters.
        /// </summary>
        public class TitleParametersProperties
        {
            /// <summary>
            /// The font family for the title.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKTitleParametersFontFamily)]
            public string FontFamily { get; set; }

            /// <summary>
            /// The font size for the title.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKTitleParametersFontSize)]
            public int FontSize { get; set; }

            /// <summary>
            /// The font style for the title.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKTitleParametersFontStyle)]
            public string FontStyle { get; set; }

            /// <summary>
            /// Boolean indicating an underline under the title.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKTitleParametersFontUnderline)]
            public bool FontUnderline { get; set; }

            /// <summary>
            /// Boolean indicating if the title is visible.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKTitleParametersShowTitle)]
            public bool ShowTitle { get; set; }

            /// <summary>
            /// Vertical alignment of the title. Possible values are "top", "bottom" and "middle".
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKTitleParametersTitleAlignment)]
            public string TitleAlignment { get; set; }

            /// <summary>
            /// Title color.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKTitleParametersTitleColor)]
            public string TitleColor { get; set; }
        }


        /// <summary>
        /// A json object describing the new title parameters.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadTitleParameters)]
        public TitleParametersProperties TitleParameters { get; set; }
    }
}

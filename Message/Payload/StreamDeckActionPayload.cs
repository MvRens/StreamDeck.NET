using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Payload
{
    /// <summary>
    /// Payload containing standard action parameters.
    /// </summary>
    public class StreamDeckActionPayload
    {
        /// <summary>
        /// The coordinates of the action triggered.
        /// </summary>
        public class CoordinatesProperties
        {
            /// <summary>
            /// The column of the action triggered.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKPayloadCoordinatesColumn)]
            public int Column { get; set;}

            /// <summary>
            /// The row of the action triggered.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKPayloadCoordinatesRow)]
            public int Row { get; set;}
        }

        /// <summary>
        /// The coordinates of the action triggered.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadCoordinates)]
        public CoordinatesProperties Coordinates { get; set;}

        /// <summary>
        /// This is a parameter that is only set when the action has multiple states defined in its manifest.json. The 0-based value contains the current state of the action.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadState)]
        public int State { get; set; }

        /// <summary>
        /// Boolean indicating if the action is inside a Multi Action.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKPayloadIsInMultiAction)]
        public bool IsInMultiAction { get; set;}
    }
}
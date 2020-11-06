using Newtonsoft.Json;
using StreamDeck.NET.Message.Interface;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when a device is plugged to the computer.
    /// </summary>
    public class StreamDeckDeviceDidConnectEventMessage : IStreamDeckDeviceMessage
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Device { get; set;  }

        /// <summary>
        /// A json object containing information about the device.
        /// </summary>
        public class DeviceInfoProperties
        {
            /// <summary>
            /// The number of columns and rows of keys that the device owns.
            /// </summary>
            public class SizeProperties
            {
                /// <summary>
                /// The number of columns of keys that the device owns.
                /// </summary>
                [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoSizeColumns)]
                public int Columns { get; set; }

                /// <summary>
                /// The number of rows of keys that the device owns.
                /// </summary>
                [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoSizeRows)]
                public int Rows { get; set; }
            }

            /// <summary>
            /// The name of the device set by the user.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoName)]
            public string Name { get; set; }

            /// <summary>
            /// Type of device.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoType)]
            public ESDSDKDeviceType Type { get; set; }

            /// <summary>
            /// The number of columns and rows of keys that the device owns.
            /// </summary>
            [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoSize)]
            public SizeProperties Size { get; set; }
        }

        /// <summary>
        /// A json object containing information about the device.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKCommonDeviceInfo)]
        public DeviceInfoProperties DeviceInfo { get; set; }
    }
}

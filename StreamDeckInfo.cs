using Newtonsoft.Json;

namespace StreamDeck.NET
{
    /// <summary>
    /// Contains the Stream Deck Info parameter as passed on the command-line.
    /// See: https://developer.elgato.com/documentation/stream-deck/sdk/registration-procedure/#Info-parameter
    /// </summary>
    #pragma warning disable 1591
    public class StreamDeckInfo
    {
        public class ApplicationInfo
        {
            [JsonProperty(StreamDeckConsts.kESDSDKApplicationInfoLanguage)]
            public string Language { get; set; }

            [JsonProperty(StreamDeckConsts.kESDSDKApplicationInfoPlatform)]
            public string Platform { get; set; }

            [JsonProperty(StreamDeckConsts.kESDSDKApplicationInfoVersion)]
            public string Version { get; set; }
        }

        [JsonProperty(StreamDeckConsts.kESDSDKApplicationInfo)]
        public ApplicationInfo Application { get; set; }


        public class PluginInfo
        {
            [JsonProperty("version")]
            public string Version { get; set; }
        }

        [JsonProperty(StreamDeckConsts.kESDSDKPluginInfo)]
        public PluginInfo Plugin { get; set; }

        [JsonProperty(StreamDeckConsts.kESDSDKDevicePixelRatio)]
        public int DevicePixelRatio { get; set; }


        public class DeviceInfo
        {
            [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoID)]
            public string ID { get; set; }

            [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoName)]
            public string Name { get; set; }

            public class DeviceSize
            {
                [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoSizeColumns)]
                public int Columns { get; set; }

                [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoSizeRows)]
                public int Rows { get; set; }
            }

            [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoSize)]
            public DeviceSize Size { get; set; }

            [JsonProperty(StreamDeckConsts.kESDSDKDeviceInfoType)]
            public ESDSDKDeviceType Type { get; set; }
        }

        [JsonProperty(StreamDeckConsts.kESDSDKDevicesInfo)]
        public DeviceInfo[] Devices { get; set; }
    }
    #pragma warning restore 1591
}

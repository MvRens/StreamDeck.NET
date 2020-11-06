using Newtonsoft.Json;

namespace StreamDeck.NET.Message.Interface
{
    /// <summary>
    /// Signifies messages that contain a device and action parameter.
    /// </summary>
    public interface IStreamDeckActionMessage : IStreamDeckEventMessage
    {
        /// <summary>
        /// The UUID of the action.
        /// </summary>
        [JsonProperty(StreamDeckConsts.kESDSDKCommonAction)]
        string Action { get; set; }
    }
}

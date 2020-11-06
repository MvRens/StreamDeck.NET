using System.Threading.Tasks;
using StreamDeck.NET.Message.Received;

namespace StreamDeck.NET
{
    /// <summary>
    /// Implement to respond to global events.
    /// An instance is created for each event that arrives.
    /// </summary>
    public interface IStreamDeckGlobalEvent
    {
        /// <summary>
        /// Called when a device is plugged to the computer.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task DeviceDidConnect(StreamDeckDeviceDidConnectEventMessage message);

        /// <summary>
        /// Called when a device is unplugged from the computer.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task DeviceDidDisconnect(StreamDeckDeviceDidDisconnectEventMessage message);

        /// <summary>
        /// Called when a monitored application is launched.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task ApplicationDidLaunch(StreamDeckApplicationDidLaunchEventMessage message);

        /// <summary>
        /// Called when a monitored application is terminated.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task ApplicationDidTerminate(StreamDeckApplicationDidTerminateEventMessage message);

        /// <summary>
        /// Called when the computer is wake up.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task SystemDidWakeUp(StreamDeckSystemDidWakeUpEventMessage message);

        /// <summary>
        /// Called after calling the getGlobalSettings API to retrieve the global persistent data stored for the plugin.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task DidReceiveGlobalSettings(StreamDeckDidReceiveGlobalSettingsEventMessage message);
    }
}

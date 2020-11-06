using System.Threading.Tasks;
using JetBrains.Annotations;
using StreamDeck.NET.Message.Received;

namespace StreamDeck.NET
{
    /// <summary>
    /// Empty implementation of IStreamDeckGlobalEvent. You do not need to use this base class,
    /// it simply provides a convenient way to override only the methods your plugin needs.
    /// For automatic registration you also need to annotate your class with the StreamDeckGlobalEvent attribute.
    /// </summary>
    [PublicAPI]
    public class StreamDeckBaseGlobalEvent : IStreamDeckGlobalEvent
    {
        /// <inheritdoc />
        public virtual Task DeviceDidConnect(StreamDeckDeviceDidConnectEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task DeviceDidDisconnect(StreamDeckDeviceDidDisconnectEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task ApplicationDidLaunch(StreamDeckApplicationDidLaunchEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task ApplicationDidTerminate(StreamDeckApplicationDidTerminateEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task SystemDidWakeUp(StreamDeckSystemDidWakeUpEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task DidReceiveGlobalSettings(StreamDeckDidReceiveGlobalSettingsEventMessage message)
        {
            return Task.CompletedTask;
        }
    }
}

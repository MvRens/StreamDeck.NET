using System;
using System.Threading.Tasks;
using StreamDeck.NET.Message.Received;

namespace StreamDeck.NET
{
    /// <summary>
    /// Implement to respond to events for a registered action.
    /// Classes implementing this interface must be annotated with the StreamDeckAction attribute.
    /// An instance is created for each context and will remain until the WillDisappear event
    /// is received for that action instance, so fields can be used to store state specific to that action instance.
    /// </summary>
    public interface IStreamDeckAction : IDisposable
    {
        /// <summary>
        /// Called when the user presses a key.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task KeyDown(StreamDeckKeyDownEventMessage message);

        /// <summary>
        /// Called when the user releases a key.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task KeyUp(StreamDeckKeyUpEventMessage message);

        /// <summary>
        /// Called when an instance of an action is displayed on the Stream Deck, for example when the
        /// hardware is first plugged in, or when a folder containing that action is entered,
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task WillAppear(StreamDeckWillAppearEventEventMessage message);

        /// <summary>
        /// Called when an instance of an action ceases to be displayed on Stream Deck,
        /// for example when switching profiles or folders
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task WillDisappear(StreamDeckWillDisappearEventEventMessage message);

        /// <summary>
        /// Called when the user changes the title or title parameters of the instance of an action.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task TitleParametersDidChange(StreamDeckTitleParametersDidChangeEventMessage message);

        /// <summary>
        /// Called after calling the getSettings API to retrieve the persistent data stored for the action.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task DidReceiveSettings(StreamDeckDidReceiveSettingsEventMessage message);

        /// <summary>
        /// Called when the Property Inspector appears.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task PropertyInspectorDidAppear(StreamDeckPropertyInspectorDidAppearEventMessage message);

        /// <summary>
        /// Called when the Property Inspector disappears.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task PropertyInspectorDidDisappear(StreamDeckPropertyInspectorDidDisappearEventMessage message);

        /// <summary>
        /// Called when the Property Inspector sends a sendToPlugin event.
        /// </summary>
        /// <param name="message">The message information passed by Stream Deck.</param>
        Task SendToPlugin(StreamDeckSendToPluginEventMessage message);
    }
}

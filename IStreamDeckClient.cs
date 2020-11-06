using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace StreamDeck.NET
{
    /// <summary>
    /// Provides access to the properties and methods for a specific action context.
    /// </summary>
    /// <remarks>
    /// When passed to an IStreamDeckGlobalEvent, not all methods are available as there is
    /// no action context. These methods are marked as "not available from a global event".
    /// Calling such a method will result in an InvalidOperationException.
    /// </remarks>
    [PublicAPI]
    public interface IStreamDeckClient
    {
        /// <summary>
        /// An opaque value identifying the instance's action.
        /// </summary>
        string ActionContext { get; }


        /// <summary>
        /// An opaque value identifying the plugin.
        /// </summary>
        string PluginUUID { get; }


        /// <summary>
        /// Save data persistently for the action's instance.
        /// </summary>
        /// <remarks>
        /// Not available from a global event.
        /// </remarks>
        /// <param name="payload">A json object which is persistently saved for the action's instance.</param>
        Task SetSettings(JObject payload);

        /// <summary>
        /// Request the persistent data for the action's instance.
        /// The plugin or Property Inspector will receive asynchronously an event didReceiveSettings containing the settings for this action.
        /// </summary>
        /// <remarks>
        /// The StreamDeckRequest helper class wraps getSettings and didReceiveSettings to provide an easier stateful method and is highly recommended.
        /// Not available from a global event.
        /// </remarks>
        Task GetSettings();

        /// <summary>
        /// Save data securely and globally for the plugin.
        /// </summary>
        /// <remarks>
        /// The StreamDeckRequest helper class wraps getSettings and didReceiveSettings to provide an easier stateful method and is highly recommended.
        /// </remarks>
        /// <param name="payload">A json object which is persistently saved globally.</param>
        Task SetGlobalSettings(JObject payload);

        /// <summary>
        /// Request the global persistent data.
        /// </summary>
        Task GetGlobalSettings();

        /// <summary>
        /// Open an URL in the default browser.
        /// </summary>
        /// <param name="url">An URL to open in the default browser.</param>
        Task OpenUrl(string url);

        /// <summary>
        /// Write a debug log to the logs file.
        /// </summary>
        /// <param name="message">A string to write to the logs file.</param>
        Task LogMessage(string message);


        /// <summary>
        /// Dynamically change the title of an instance of an action.
        /// </summary>
        /// <remarks>
        /// Not available from a global event.
        /// </remarks>
        /// <param name="title">The title to display. If there is no title parameter, the title is reset to the title set by the user.</param>
        /// <param name="target">Specify if you want to display the title on the hardware and software (0), only on the hardware (1) or only on the software (2). Default is 0.</param>
        /// <param name="state">A 0-based integer value representing the state of an action with multiple states. This is an optional parameter. If not specified, the title is set to all states.</param>
        Task SetTitle(string title, ESDSDKTarget target, int? state);

        /// <summary>
        /// Dynamically change the image displayed by an instance of an action.
        /// </summary>
        /// <remarks>
        /// Not available from a global event.
        /// </remarks>
        /// <param name="image">The image to display encoded in base64 with the image format declared in the mime type (PNG, JPEG, BMP, ...). svg is also supported. If no image is passed, the image is reset to the default image from the manifest.</param>
        /// <param name="target">Specify if you want to display the image on the hardware and software (0), only on the hardware (1) or only on the software (2). Default is 0.</param>
        /// <param name="state">A 0-based integer value representing the state of an action with multiple states. This is an optional parameter. If not specified, the image is set to all states.</param>
        Task SetImage(string image, ESDSDKTarget target, int? state);

        /// <summary>
        /// Temporarily show an alert icon on the image displayed by an instance of an action.
        /// </summary>
        /// <remarks>
        /// Not available from a global event.
        /// </remarks>
        Task ShowAlert();

        /// <summary>
        /// Temporarily show an OK checkmark icon on the image displayed by an instance of an action.
        /// </summary>
        /// <remarks>
        /// Not available from a global event.
        /// </remarks>
        Task ShowOk();

        /// <summary>
        /// Change the state of the action's instance supporting multiple states.
        /// </summary>
        /// <remarks>
        /// Not available from a global event.
        /// </remarks>
        /// <param name="state">A 0-based integer value representing the state requested.</param>
        Task SetState(int state);

        /// <summary>
        /// Switch to one of the preconfigured read-only profiles.
        /// </summary>
        /// <param name="device">An opaque value identifying the device.</param>
        /// <param name="profileName">The name of the profile to switch to. The name should be identical to the name provided in the manifest.json file.</param>
        Task SwitchToProfile(string device, string profileName);

        /// <summary>
        /// Send a payload to the Property Inspector.
        /// </summary>
        /// <remarks>
        /// Not available from a global event.
        /// </remarks>
        /// <param name="action">The action unique identifier.</param>
        /// <param name="payload">A json object that will be received by the Property Inspector.</param>
        Task SendToPropertyInspector(string action, JObject payload);
    }
}

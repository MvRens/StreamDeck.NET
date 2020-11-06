using JetBrains.Annotations;

namespace StreamDeck.NET.Attribute
{
    /// <summary>
    /// Annotate a class with the StreamDeckAction attribute to respond to events for a registered action.
    /// The annotated class must implement IStreamDeckAction.
    /// </summary>
    [MeansImplicitUse(ImplicitUseTargetFlags.Itself)]
    public class StreamDeckActionAttribute : System.Attribute
    {
        /// <summary>
        /// The UUID of the action as specified in the manifest.json
        /// </summary>
        public string UUID { get; }


        /// <summary>
        /// Annotate a class with the StreamDeckAction attribute to respond to events for a registered action.
        /// The annotated class must implement IStreamDeckAction.
        /// </summary>
        /// <param name="uuid">The UUID of the action as specified in the manifest.json</param>
        public StreamDeckActionAttribute(string uuid)
        {
            UUID = uuid;
        }
    }
}

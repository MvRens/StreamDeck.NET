using JetBrains.Annotations;

namespace StreamDeck.NET.Attribute
{
    /// <summary>
    /// Annotate a class with the StreamDeckGlobalEvent attribute to register is as a handler to events
    /// which are not specific to an action when RegisterAll is called. This annotation is optional
    /// if you manually call RegisterGlobalEvent on the StreamDeckApplication.
    /// The annotated class must implement IStreamDeckGlobalEvent.
    /// </summary>
    [MeansImplicitUse(ImplicitUseTargetFlags.Itself)]
    public class StreamDeckGlobalEventAttribute : System.Attribute
    {
    }
}

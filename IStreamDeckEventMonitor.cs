namespace StreamDeck.NET
{
    /// <summary>
    /// Implement and register to be notified of all incoming events. Combines IStreamDeckAction and IStreamDeckGlobalEvent.
    /// An instance is created for each event that arrives.
    /// </summary>
    public interface IStreamDeckEventMonitor : IStreamDeckAction, IStreamDeckGlobalEvent
    {
    }
}

using System.Diagnostics;

namespace StreamDeck.NET.Logger
{
    /// <summary>
    /// Logs to the debug output window.
    /// </summary>
    public class DebugLogger : BaseSimpleStringLogger
    {
        /// <summary>
        /// Creates a logger which outputs to the debug log.
        /// </summary>
        public DebugLogger() : base(true) { }


        /// <inheritdoc />
        protected override void Log(string message)
        {
            Debug.Print(message);
        }
    }
}

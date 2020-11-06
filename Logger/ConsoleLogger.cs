using System;
using JetBrains.Annotations;

namespace StreamDeck.NET.Logger
{
    /// <summary>
    /// Logs to the Console.
    /// </summary>
    [PublicAPI]
    public class ConsoleLogger : BaseSimpleStringLogger
    {
        /// <summary>
        /// Creates a new logger which outputs to the console.
        /// </summary>
        /// <param name="verbose">Determines if verbose messages are also logged.</param>
        public ConsoleLogger(bool verbose) : base(verbose)
        {
        }

        /// <inheritdoc />
        protected override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}

﻿using StreamDeck.NET.Message.Interface;
using StreamDeck.NET.Message.Payload;

namespace StreamDeck.NET.Message.Received
{
    /// <summary>
    /// Received when the user presses a key.
    /// </summary>
    public class StreamDeckKeyDownEventMessage : IStreamDeckActionMessage, IStreamDeckContextMessage, IStreamDeckDeviceMessage, IStreamDeckPayloadMessage<StreamDeckActionSettingsPayload>
    {
        /// <inheritdoc />
        public string Event { get; set; }

        /// <inheritdoc />
        public string Action { get; set; }

        /// <inheritdoc />
        public string Context { get; set; }

        /// <inheritdoc />
        public string Device { get; set; }

        /// <inheritdoc />
        public StreamDeckActionSettingsPayload Payload { get; set; }
    }
}

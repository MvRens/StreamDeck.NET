using System.Threading.Tasks;
using JetBrains.Annotations;
using StreamDeck.NET.Message.Received;

namespace StreamDeck.NET
{
    /// <summary>
    /// Empty implementation of IStreamDeckAction. You do not need to use this base class,
    /// it simply provides a convenient way to override only the methods your plugin needs.
    /// Note that you also need to annotate your class with the StreamDeckAction attribute.
    /// </summary>
    [PublicAPI]
    public class StreamDeckBaseAction : IStreamDeckAction
    {
        /// <inheritdoc />
        public virtual Task KeyDown(StreamDeckKeyDownEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task KeyUp(StreamDeckKeyUpEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task WillAppear(StreamDeckWillAppearEventEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task WillDisappear(StreamDeckWillDisappearEventEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task TitleParametersDidChange(StreamDeckTitleParametersDidChangeEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task DidReceiveSettings(StreamDeckDidReceiveSettingsEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task PropertyInspectorDidAppear(StreamDeckPropertyInspectorDidAppearEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task PropertyInspectorDidDisappear(StreamDeckPropertyInspectorDidDisappearEventMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
        }
    }
}

using System;
using VideoStreaming.Base.Annotations;

namespace VideoStreaming.WPF.Views
{
    public interface IStreamer
    {
        void Play(Uri channel);
    }
}
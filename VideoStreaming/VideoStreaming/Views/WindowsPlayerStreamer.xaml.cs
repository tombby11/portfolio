using System;

namespace VideoStreaming.WPF.Views
{
    /// <summary>
    ///     Interaction logic for WindowsPlayerStreamer.xaml
    /// </summary>
    public partial class WindowsPlayerStreamer : IStreamer
    {
        public WindowsPlayerStreamer()
        {
            InitializeComponent();
        }

        public void Play(Uri channel)
        {
            Player.Stop();
            Player.Source = channel;
            Player.Play();
        }
    }
}
using System;
using System.Windows;
using VideoStreaming.Base.Models;
using VideoStreaming.Base.ViewModels;
using VideoStreaming.WPF.Views;

namespace VideoStreaming.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IStreamer _streamer; 
        public MainWindow(Streamer streamerType)
        {
            var viewModel = new VideoStreamingViewModel();
            viewModel.ChannelChanged += OnChannelChanged;
            _streamer  = GetStreamer(streamerType);
            DataContext = viewModel; 
            InitializeComponent();

            switch (streamerType)
            {
                case Streamer.WindowsMediaPlayer:
                    Grid.Children.Add((WindowsPlayerStreamer) _streamer);
                  
                    break;
                case Streamer.Vlc:
                    Grid.Children.Add((VlcStreamer)_streamer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(streamerType), streamerType, null);
            }
        }


        private void OnChannelChanged(object sender, Channel channel)
        {
            _streamer.Play(channel.DefaultSource);
        }

        private IStreamer GetStreamer(Streamer streamerType)
        {
          
            switch (streamerType)
            {
                case Streamer.WindowsMediaPlayer:
                    var wPStreamer = new WindowsPlayerStreamer();
                    System.Windows.Controls.Grid.SetRow(wPStreamer, 0);
                    System.Windows.Controls.Grid.SetColumn(wPStreamer, 1);
                    return wPStreamer;
                    break;
                case Streamer.Vlc:
                    var vlcStreamer = new VlcStreamer();
                    System.Windows.Controls.Grid.SetRow(vlcStreamer, 0);
                    System.Windows.Controls.Grid.SetColumn(vlcStreamer, 1);
                    return vlcStreamer;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(streamerType), streamerType, null);
            }
        }
    }
}

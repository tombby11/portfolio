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
        public MainWindow()
        {
            var viewModel = new VideoStreamingViewModel();
            viewModel.ChannelChanged += OnChannelChanged;
            DataContext = viewModel; 
            InitializeComponent();
        }

        private void OnChannelStopped(object sender, Channel channel)
        {
            StreamingScreen.MyMediaElement.Close();
        }

        private void OnChannelChanged(object sender, Channel channel)
        {
            StreamingScreen.MyMediaElement.Close();
            StreamingScreen.MyMediaElement.Source = channel.DefaultSource;
            StreamingScreen.MyMediaElement.Play();
        }
    }
}

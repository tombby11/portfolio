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

        private void OnChannelChanged(object sender, Channel channel)
        {
            StreamingScreen.MyMediaElement.Stop();
            StreamingScreen.MyMediaElement.Source = channel.DefaultSource;
            StreamingScreen.MyMediaElement.Play();
        }

        private void StreamerScreen_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ControlBar.Visibility = Visibility.Visible;
        }

        private void StreamerScreen_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ControlBar.Visibility = Visibility.Hidden;
        }
    }
}

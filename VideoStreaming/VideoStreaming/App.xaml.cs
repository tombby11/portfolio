using System.Windows;

namespace VideoStreaming.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow(Streamer.Vlc) ;
            mainWindow.Show();
        }
    }
}

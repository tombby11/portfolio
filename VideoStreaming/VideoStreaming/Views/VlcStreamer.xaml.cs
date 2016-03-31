using System;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using  Vlc.DotNet.Wpf;
using VlcLibDirectoryNeededEventArgs = Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs;

namespace VideoStreaming.WPF.Views
{
    /// <summary>
    /// Interaction logic for VlcStreamer.xaml
    /// </summary>
    public partial class VlcStreamer : UserControl, IStreamer
    {
        public VlcStreamer()
        {
            
            InitializeComponent();
            Player.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            Player.MediaPlayer.EndInit();
        }

        private void OnVlcControlNeedsLibDirectory(object sender, VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            var parentDirectory = System.IO.Directory.GetParent(currentDirectory).FullName;
            //packages\Vlc.DotNet.Wpf.2.1.122\lib\net45\x86
            if (currentDirectory == null)
                return;
            if (AssemblyName.GetAssemblyName(currentAssembly.Location).ProcessorArchitecture == ProcessorArchitecture.X86)
                e.VlcLibDirectory = new DirectoryInfo(Path.Combine(parentDirectory, @"Lib\x86"));
            else
                e.VlcLibDirectory = new DirectoryInfo(Path.Combine(parentDirectory, @"Lib\x64"));
        }

        public void Play(Uri channel)
        {
            Player.MediaPlayer.Stop();
            Player.MediaPlayer.Play(channel);
        }
    }
}

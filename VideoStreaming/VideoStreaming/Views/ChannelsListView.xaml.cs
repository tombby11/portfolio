using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoStreaming.Base.Models;
using VideoStreaming.Base.ViewModels;

namespace VideoStreaming.WPF.Views
{
    /// <summary>
    /// Interaction logic for ChannelsListView.xaml
    /// </summary>
    public partial class ChannelsListView : UserControl
    {
        public ChannelsListView()
        {
            InitializeComponent();
        }

        private void MainChannelsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            var selectedChannel = e.AddedItems[0] as Channel;
            var viewModel = this.DataContext as VideoStreamingViewModel;
            viewModel.ChangeChannelCommand.Execute(selectedChannel);
        }
    }
}

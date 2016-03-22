using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.Commands;
using VideoStreaming.Base.Annotations;
using VideoStreaming.Base.Models;

namespace VideoStreaming.Base.ViewModels
{
    public class VideoStreamingViewModel : INotifyPropertyChanged
    {
        #region Private Fields 

        private int _selectedChannelIndex;

        #endregion

        public VideoStreamingViewModel()
        {
            Channels = new ObservableCollection<Channel>
            {
                new Channel
                {
                    Name = "MBC1",
                    DefaultSource = new Uri("http://star.010e.net:8000/live/ahmedmadi/123456/71.ts"),
                    HighDefinitionSource = new Uri("http://star.010e.net:8000/live/ahmedmadi/123456/71.ts")
                },
                new Channel
                {
                    Name = "MBC2",
                    DefaultSource = new Uri("http://star.010e.net:8000/live/ahmedmadi/123456/72.ts"),
                    HighDefinitionSource = new Uri("http://star.010e.net:8000/live/ahmedmadi/123456/72.ts")
                }
            };

            PlayChannelCommand = new DelegateCommand(ExecutePlayChannelCommand);
            NextChannelCommand = new DelegateCommand(ExecuteNextChannel, CanExecuteNextChannelCommand);
            PreviousChannelCommand = new DelegateCommand(ExecutePreviousChannelCommand, CanExecutePreviousChannelCommand);
        }

        public ObservableCollection<Channel> Channels { get; set; }

        public int SelectedChannelIndex
        {
            get { return _selectedChannelIndex; }
            set
            {
                _selectedChannelIndex = value;
                NextChannelCommand.RaiseCanExecuteChanged();
                PreviousChannelCommand.RaiseCanExecuteChanged();
            }
        }

        #region Events

        public event EventHandler<Channel> ChannelChanged;

        #endregion

        #region PlayChannelCommand

        public DelegateCommand PlayChannelCommand { get; set; }

        private void ExecutePlayChannelCommand()
        {
            if (Channels[SelectedChannelIndex] != null)
            {
                ChannelChanged?.Invoke(this, Channels[SelectedChannelIndex]);
            }
        }

        #endregion

        #region NextChannelCommand

        public DelegateCommand NextChannelCommand { get; set; }

        private void ExecuteNextChannel()
        {
            if (SelectedChannelIndex > Channels.Count-1) return;

            SelectedChannelIndex++;
            ChannelChanged?.Invoke(this, Channels[SelectedChannelIndex]);
        }

        private bool CanExecuteNextChannelCommand()
        {
            return SelectedChannelIndex < Channels.Count-1;
        }

        #endregion

        #region PreviousChannelCommand

        public DelegateCommand PreviousChannelCommand { get; set; }

        private void ExecutePreviousChannelCommand()
        {
            if (SelectedChannelIndex <= 0) return;

            SelectedChannelIndex--;
            ChannelChanged?.Invoke(this, Channels[SelectedChannelIndex]);
        }

        private bool CanExecutePreviousChannelCommand()
        {
            return SelectedChannelIndex > 0;
        }

        #endregion

        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
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

        #endregion


        #region Events

        public event EventHandler<Channel> ChannelChanged;

        #endregion

        public VideoStreamingViewModel()
        {
            Channels = new ObservableCollection<Channel>
            {
                new Channel
                {
                    Name = "MBC1",
                    CurrentDefinitionQuality = DefinitionQuality.HD,
                    DefaultSource = new Uri("http://star.010e.net:8000/live/ahmedmadi/123456/71.ts"),
                    HighDefinitionSource = new Uri("http://star.010e.net:8000/live/ahmedmadi/123456/71.ts"),
                    ImageSrouce = new Uri("https://pbs.twimg.com/profile_images/592403560756719617/LJPsFJ5M.jpg")
                },
                new Channel
                {
                    Name = "MBC2",
                    CurrentDefinitionQuality = DefinitionQuality.Standard,
                    DefaultSource = new Uri("http://star.010e.net:8000/live/ahmedmadi/123456/72.ts"),
                    HighDefinitionSource = new Uri("http://star.010e.net:8000/live/ahmedmadi/123456/72.ts"),
                    ImageSrouce = new Uri("http://2.bp.blogspot.com/-S5CJEILUHHY/Vp1Hz1ZOyNI/AAAAAAAAA8Q/k9_kfjNOv-Y/s1600/MBC2_Logo.png")
                }
            };
      }

        public ObservableCollection<Channel> Channels { get; set; }


        #region ChangeChannelQuality

        public DelegateCommand<DefinitionQuality> ChangeChannelQuality { get; set; }

        #endregion 


        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public void ChangeChannel(Channel selectedChannel)
        {
            ChannelChanged?.Invoke(this, selectedChannel);
        }
    }
}
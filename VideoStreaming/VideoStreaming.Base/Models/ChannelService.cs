using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreaming.Base.Models
{
    public class ChannelService
    {
        public Dictionary<string,Channel> Channels { get; set; }

        public void AddChannel(Channel channel)
        {
            if (!Channels.ContainsKey(channel.Id))
            {
                Channels.Add(channel.Id,channel);
            }         
        }

        public void RemoveChannel(Channel channel)
        {
            if (Channels.ContainsKey(channel.Id)) {
                Channels.Remove(channel.Id);
            }
        }

        public Channel GetChannel(string id)
        {
            return Channels.ContainsKey(id) ? Channels[id] : null;
        }
    }
}

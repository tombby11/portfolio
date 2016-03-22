using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreaming.Base.Models
{
    public class Channel
    {
       public enum DefinitionQuality
        {
            Standard,
            HD,
            FullHD        
        }
        public string Name { get; set; }
        public string Id { get; set; }

        public Uri ImageSrouce { get; set; }
        public Uri DefaultSource { get; set; }
        public Uri StandardDefinitionSource { get; set; }
        public Uri HighDefinitionSource { get; set; }
        public Uri FullHighDefinitionSource { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyssaFrederickMusic
{
    public class track
    {
        [DataMember(Name = "name")]
        public string name;

        [DataMember(Name = "preview_url")]
        public string preview_url;
    }
}

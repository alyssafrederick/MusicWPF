using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyssaFrederickMusic
{
    [DataContract]
    public class artistInfo
    {
        [DataMember(Name = "id")]
        public string id;

        [DataMember(Name = "name")]
        public string name;

        [DataMember(Name = "popularity")]
        public string popularity;

        [DataMember(Name = "images")]
        public images[] images;
    }
}

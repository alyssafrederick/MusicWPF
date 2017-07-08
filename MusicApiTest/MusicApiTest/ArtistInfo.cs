using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicApiTest
{
    [DataContract]
    public class ArtistInfo
    {
        [DataMember(Name = "id")]
        public string id;

        [DataMember(Name = "name")]
        public string name;

        [DataMember(Name = "popularity")]
        public int popularity;

        [DataMember(Name = "images")]
        public Images[] images;
    }
}

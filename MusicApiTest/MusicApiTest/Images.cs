using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicApiTest
{
    public class Images
    {
        [DataMember(Name = "height")]
        public int height;

        [DataMember(Name = "width")]
        public int width;

        [DataMember(Name = "url")]
        public string url;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicApiTest
{
    public class Track
    {
        [DataMember(Name = "name")]
        public string name;

        [DataMember(Name = "preview_url")]
        public string preview_url;
    }
}

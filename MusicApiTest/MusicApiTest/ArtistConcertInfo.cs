using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MusicApiTest
{
    public class ArtistConcertInfo
    {
        [DataMember(Name = "name")]
        public string name;

        [DataMember(Name = "url")]
        public string url;

        [DataMember(Name = "mbid")]
        public string mbid;

        [DataMember(Name = "upcoming_events_count")]
        public string upcomingEvents;

        public override string ToString()
        {
            return string.Format($"{name}\n{url}\n{mbid}\n{upcomingEvents}");
        }
    }
}

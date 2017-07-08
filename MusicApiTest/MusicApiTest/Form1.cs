using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MusicApiTest
{
    public partial class Form1 : Form
    {
        //http://developer.echonest.com/docs/v4/artist.html
        //https://www.bandsintown.com/api/1.0

        string ApplicationName = "MusicApiTest";
        string ArtistName = "Diplo";
        ArtistConcertInfo artist;
        WebClient client = new WebClient();
        ArtistInfo[] artistInfo = new ArtistInfo[0];
        WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();
        Track[] tracks;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ConcertData = client.DownloadString(string.Format($@"http://api.bandsintown.com/artists/{comboBox1.Text}.json?app_id={ApplicationName}"));
                artist = JsonConvert.DeserializeObject<ArtistConcertInfo>(ConcertData);

                JObject artistData = JObject.Parse(client.DownloadString(string.Format($@"https://api.spotify.com/v1/search?q={comboBox1.Text}&type=artist&limit=50")));

                //var artistInfo = JsonConvert.DeserializeObject<ArtistInfo>(artistData);

                artistInfo = JsonConvert.DeserializeObject<ArtistInfo[]>(artistData.SelectToken("artists.items").ToString());

                label1.Text = artistInfo[0].name;

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                pictureBox1.Image = Image.FromStream(new MemoryStream(client.DownloadData(artistInfo[0].images[0].url)));

                JObject topTracks = JObject.Parse(client.DownloadString(string.Format($@"https://api.spotify.com/v1/artists/{artistInfo[0].id}/top-tracks?country=US")));


                tracks = JsonConvert.DeserializeObject<Track[]>(topTracks.SelectToken("tracks").ToString());

                comboBox2.Items.Clear();
                foreach (Track track in tracks)
                {
                    comboBox2.Items.Add(track.name);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("No Artist Found");
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                JObject artistData = JObject.Parse(client.DownloadString(string.Format($@"https://api.spotify.com/v1/search?q={comboBox1.Text}&type=artist&limit=50")));

                //var artistInfo = JsonConvert.DeserializeObject<ArtistInfo>(artistData);

                var info = JsonConvert.DeserializeObject<ArtistInfo[]>(artistData.SelectToken("artists.items").ToString());
                foreach (ArtistInfo inf in info)
                {
                    comboBox1.AutoCompleteCustomSource.Add(inf.name);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Play
            foreach(Track track in tracks)
            {
                if(track.name == comboBox2.Text)
                {
                    Player.controls.stop();
                    Player.URL = track.preview_url;
                    Player.controls.play();
                }
            }
        }
        
    }
}

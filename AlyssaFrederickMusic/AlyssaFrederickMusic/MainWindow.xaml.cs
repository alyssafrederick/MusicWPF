using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AlyssaFrederickMusic
{

    public partial class MainWindow : Window
    {
        string applicationName = "AlyssaFrederickMusic";
        string artistName = "artist";
        artistConcertInfo artist;
        WebClient client = new WebClient();
        artistInfo[] artistinfo = new artistInfo[0];
        WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();
        track[] tracks;
        double mousex;
        bool isPlaying = false;

        System.Windows.Threading.DispatcherTimer searchPanelTimer = new System.Windows.Threading.DispatcherTimer();

        //public object JsonConvert { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            searchPanelTimer.Tick += SearchPanelTimer_Tick;
            searchPanelTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            searchPanelTimer.Start();
        }

        private void SearchPanelTimer_Tick(object sender, EventArgs e)
        {
            //int offset = 15;
            if (mousex <= 20 || searchCanvas.Margin.Left + searchCanvas.Width > mousex)
            {
                if (searchCanvas.Margin.Left < 0)
                {
                    searchCanvas.Margin = new Thickness(searchCanvas.Margin.Left + 5, searchCanvas.Margin.Top, 0, 0);
                }
                else
                {
                    searchCanvas.Margin = new Thickness(0, searchCanvas.Margin.Top, 0, 0);
                }
            }
            else if (searchCanvas.Margin.Left + searchCanvas.Width > 0)
            {
                searchCanvas.Margin = new Thickness(searchCanvas.Margin.Left - 5, searchCanvas.Margin.Top, 0, 0);
                searchComboBox.IsDropDownOpen = false;
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            mousex = e.GetPosition(this).X;
            //if (mousex != listenComboBox)
            //{ 
            //    listenComboBox.IsDropDownOpen = false;
            //}
        }

        private void playVSpause_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isPlaying = !isPlaying;
            if (isPlaying)
            {
                playVSpause.Source = new BitmapImage(new Uri("Resources/pause.png", UriKind.Relative));
                foreach (track track in tracks)
                {
                    if (track.name == listenComboBox.Text)
                    {
                        Player.controls.stop();
                        Player.URL = track.preview_url;
                        Player.controls.play();
                    }
                }
            }
            else
            {
                playVSpause.Source = new BitmapImage(new Uri("Resources/play.png", UriKind.Relative));
                Player.controls.stop();
            }
        }

        private void listenComboBox_DragLeave(object sender, DragEventArgs e)
        {
            listenComboBox.IsDropDownOpen = false;
        }

        private void searchComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    string concertData = client.DownloadString(string.Format($@"http://api.bandsintown.com/artists/{searchComboBox.Text}.json?app_id={applicationName}"));
                    artist = JsonConvert.DeserializeObject<artistConcertInfo>(concertData);

                    JObject artistData = JObject.Parse(client.DownloadString(string.Format($@"https://api.spotify.com/v1/search?q={searchComboBox.Text}&type=artist&limit=50")));
                    artistinfo = JsonConvert.DeserializeObject<artistInfo[]>(artistData.SelectToken("artists.items").ToString());

                    artistNameLabel.Content = artistinfo[0].name;
                    BitmapImage a = new BitmapImage(new Uri(artistinfo[0].images[0].url));
                    artistImage.Source = a;

                    JObject topTracks = JObject.Parse(client.DownloadString(string.Format($@"https://api.spotify.com/v1/artists/{artistinfo[0].id}/top-tracks?country=US")));
                    tracks = JsonConvert.DeserializeObject<track[]>(topTracks.SelectToken("tracks").ToString());

                    //JObject concerts = JObject.Parse(client.DownloadString(string.Format($@"http://api.bandsintown.com/artists/Skrillex/events.json?api_version=2.0&app_id=YOUR_APP_ID")));
                    //upcomingconcerts = JsonConvert.DeserializeObject<>

                    listenComboBox.Items.Clear();
                    foreach (track track in tracks)
                    {
                        listenComboBox.Items.Add(track.name);
                    }

                    
                }

                catch(Exception exception)
                {
                    MessageBox.Show("no artist found");
                }
            }
        }

        private void searchComboBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (searchComboBox.Items[0] != "")
            {
                JObject artistData = JObject.Parse(client.DownloadString(string.Format($@"https://api.spotify.com/v1/search?q={searchComboBox.Text}&type=artist&limit=50")));

                var info = JsonConvert.DeserializeObject<artistInfo[]>(artistData.SelectToken("artists.items").ToString());
                //foreach (artistInfo inf in info)
                //{
                //    searchComboBox.
                //}

            }
        }
    }
}

using CatalogMusic.Entity;
using CatalogMusic.Patterns;
using System.Windows.Forms;

namespace CatalogMusic
{
    public partial class Form1 : Form
    {
        private Facade _facade;
        private Album newAlbum = null;
        private Playlist newPlaylist = null;

        public Form1()
        {
            InitializeComponent();
            _facade = new Facade(new BuilderSearch(null), new SearchEngine(null, null, null));
            _facade.Load();
            print();
        }

        #region methods_print

        private void print()
        {
            printArtists();
            printGenres();
            printTracks();
            printAlbums();
            printPlaylists();
        }

        private void printArtists()
        {
            listBox1.Items.Clear();
            comboBox1.Items.Clear();
            comboBox3.Items.Clear();
            foreach (var artist in _facade.Artists)
            {
                listBox1.Items.Add(artist);
                comboBox1.Items.Add(artist);
                comboBox3.Items.Add(artist);
            }
        }

        private void printGenres()
        {
            listBox2.Items.Clear();
            comboBox2.Items.Clear();
            foreach (var genre in _facade.Genres)
            {
                listBox2.Items.Add(genre);
                comboBox2.Items.Add(genre);
            }
        }

        private void printTracks()
        {
            listBox3.Items.Clear();
            listBox8.Items.Clear();
            foreach (var track in _facade.Tracks)
            {
                listBox3.Items.Add(track);
                listBox8.Items.Add(track);
            }
            _facade.UpdateSearchEngine<Track>(_facade.Tracks);
        }

        private void printTrackInAlbum(Album album)
        {
            listBox6.Items.Clear();
            foreach (var track in album.Tracks)
            {
                listBox6.Items.Add(track);
            }
        }

        private void printTrackInPlaylist(Playlist playlist)
        {
            listBox9.Items.Clear();
            foreach (var track in playlist.Tracks)
            {
                listBox9.Items.Add(track);
            }
        }

        private void printAlbums()
        {
            listBox4.Items.Clear();
            foreach (var album in _facade.Albums)
            {
                listBox4.Items.Add(album);
            }
            listBox6.Items.Clear();
            _facade.UpdateSearchEngine<Album>(_facade.Albums);
        }

        private void printPlaylists()
        {
            listBox7.Items.Clear();
            foreach (var playlist in _facade.Playlists)
            {
                listBox7.Items.Add(playlist);
            }
            listBox9.Items.Clear();
            _facade.UpdateSearchEngine<Playlist>(_facade.Playlists);
        }

        #endregion

        #region methods_form
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                Artist artist = new Artist(textBox1.Text);
                _facade.Artists.Add(artist);
                printArtists();
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _facade.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                _facade.Artists.Remove(listBox1.SelectedItem as Artist);
                printArtists();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                Genre genre = new Genre(textBox2.Text);
                _facade.Genres.Add(genre);
                printGenres();
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                _facade.Genres.Remove(listBox2.SelectedItem as Genre);
                printGenres();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && !string.IsNullOrEmpty(textBox3.Text))
            {
                Track track = new Track(textBox3.Text, comboBox1.SelectedItem as Artist, comboBox2.SelectedItem as Genre);
                _facade.Tracks.Add(track);
                printTracks();
                textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                _facade.Tracks.Remove(listBox3.SelectedItem as Track);
                printTracks();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null && !string.IsNullOrEmpty(textBox4.Text))
            {
                if (newAlbum == null)
                {
                    newAlbum = new Album(textBox4.Text, comboBox1.SelectedItem as Artist);
                }
                newAlbum.AddTrack(listBox5.SelectedItem as Track);
                printTrackInAlbum(newAlbum);
            }
            else
            {
                MessageBox.Show("Не все поля заполнены", "Ошибка");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox5.Items.Clear();
            if ((sender as ComboBox).SelectedItem != null)
            {
                listBox6.Items.Clear();
                listBox4.SelectedIndex = -1;
                var artist = (sender as ComboBox).SelectedItem as Artist;
                var tracks = _facade.Tracks.Where(x => x.Artist.Nickname == artist.Nickname).ToList();
                foreach (var track in tracks)
                {
                    listBox5.Items.Add(track);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox6.SelectedItem != null)
            {
                if (newAlbum != null)
                {
                    newAlbum.RemoveTrack(listBox6.SelectedItem as Track);
                    printTrackInAlbum(newAlbum);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (newAlbum != null && newAlbum.Tracks.Count > 0)
            {
                _facade.Albums.Add(newAlbum);
                printAlbums();
                newAlbum = null;
                listBox6.Items.Clear();
            }
            else
            {
                MessageBox.Show("Не добавлены песни в альбом", "Ошибка");
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                listBox6.Items.Clear();
                newAlbum = (sender as ListBox).SelectedItem as Album;
                var album = (sender as ListBox).SelectedItem as Album;
                foreach (var track in album.Tracks)
                {
                    listBox6.Items.Add(track);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as TabControl).SelectedTab.Text != "Альбомы")
            {
                newAlbum = null;
                listBox4.SelectedIndex = -1;
                listBox6.Items.Clear();
            }
            if ((sender as TabControl).SelectedTab.Text != "Плейлисты")
            {
                newPlaylist = null;
                listBox7.SelectedIndex = -1;
                listBox9.Items.Clear();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedItem != null)
            {
                _facade.Albums.Remove(listBox4.SelectedItem as Album);
                printAlbums();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox8.SelectedItem != null)
            {
                if (newPlaylist == null)
                {
                    newPlaylist = new Playlist(textBox5.Text);
                }
                newPlaylist.AddTrack(listBox8.SelectedItem as Track);
                printTrackInPlaylist(newPlaylist);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItem != null)
            {
                if (newPlaylist != null)
                {
                    newPlaylist.RemoveTrack(listBox9.SelectedItem as Track);
                    printTrackInPlaylist(newPlaylist);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (newPlaylist.Tracks.Count > 0)
            {
                _facade.Playlists.Add(newPlaylist);
                printPlaylists();
            }
            else
            {
                MessageBox.Show("Не добавлены песни в плейлист", "Ошибка");
            }
        }
        
        private void button14_Click(object sender, EventArgs e)
        {
            if (listBox7.SelectedItem != null)
            {
                _facade.Playlists.Remove(listBox7.SelectedItem as Playlist);
                printPlaylists();
            }
        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
            {
                listBox9.Items.Clear();
                newPlaylist = (sender as ListBox).SelectedItem as Playlist;
                var playlist = (sender as ListBox).SelectedItem as Playlist;
                foreach (var track in playlist.Tracks)
                {
                    listBox9.Items.Add(track);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var artists = _facade.FindArtists(textBox6.Text);
            listBox10.Items.Clear();
            foreach (var artist in artists)
            {
                listBox10.Items.Add(artist);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var albums = _facade.FindAlbum(textBox7.Text);
            listBox10.Items.Clear();
            foreach (var album in albums)
            {
                listBox10.Items.Add(album);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var playlists = _facade.FindPlaylist(textBox8.Text);
            listBox10.Items.Clear();
            foreach (var playlist in playlists)
            {
                listBox10.Items.Add(playlist);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            BuilderSearch builderSearch = new BuilderSearch(_facade.Tracks);
            _facade.SetBuildSearch(builderSearch);   

            if (!string.IsNullOrEmpty(textBox9.Text))
            {
                _facade.SetCriterioNickname(textBox9.Text);
            }

            if (!string.IsNullOrEmpty(textBox10.Text))
            {
                _facade.SetCriterioGenre(textBox10.Text);
            }

            if (!string.IsNullOrEmpty(textBox11.Text))
            {
                _facade.SetCriterioTitleTrack(textBox11.Text);
            }

            listBox11.Items.Clear();
            foreach(var track in _facade.GetResult())
            {
                listBox11.Items.Add(track);
            }
        }
        #endregion
    }
}

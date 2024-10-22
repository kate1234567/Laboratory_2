using CatalogMusic.Entity;
using Newtonsoft.Json;
namespace CatalogMusic.Patterns
{
    public class Facade
    {
        private BuilderSearch _builderSearch;
        private SearchEngine _searchEngine;

        public List<Artist> Artists = new List<Artist>();
        public List<Genre> Genres = new List<Genre>();
        public List<Track> Tracks = new List<Track>();
        public List<Album> Albums = new List<Album>();
        public List<Playlist> Playlists = new List<Playlist>();

        public void UpdateSearchEngine<T>(List<T> data)
        {
            if (typeof(T) == typeof(Track))
            {
                _searchEngine._tracks = data as List<Track>;
            }
            if (typeof(T) == typeof(Album))
            {
                _searchEngine._albums = data as List<Album>;
            }
            if (typeof(T) == typeof(Playlist))
            {
                _searchEngine._playlists = data as List<Playlist>;
            }
        }

        public Facade(BuilderSearch builderSearch, SearchEngine searchEngine)
        {
            _builderSearch = builderSearch;
            _searchEngine = searchEngine;
        }

        public void SetBuildSearch(BuilderSearch builderSearch)
        {
            _builderSearch = builderSearch;
        }

        public void SetCriterioNickname(string nickname)
        {
            _builderSearch.SetCriterioNicknameArtist(nickname);
        }

        public void SetCriterioGenre(string genre)
        {
            _builderSearch.SetCriterioGenre(genre);
        }

        public void SetCriterioTitleTrack(string title)
        {
            _builderSearch.SetCriterioTitleTrack(title);
        }

        public List<Track> GetResult()
        {
            return _builderSearch.GetResult();
        }

        public List<Artist> FindArtists(string nickname)
        {
            return _searchEngine.FindArtists(nickname);
        }

        public List<Album> FindAlbum(string title)
        {
            return _searchEngine.FindAlbums(title);
        }

        public List<Playlist> FindPlaylist(string title)
        {
            return _searchEngine.FindPlaylists(title);
        }

        public void Load()
        {
            if (File.Exists("Artists.txt"))
            {
                var text = File.ReadAllText("Artists.txt");
                Artists = JsonConvert.DeserializeObject<List<Artist>>(text);
            }

            if (File.Exists("Genres.txt"))
            {
                var text = File.ReadAllText("Genres.txt");
                Genres = JsonConvert.DeserializeObject<List<Genre>>(text);
            }

            if (File.Exists("Tracks.txt"))
            {
                var text = File.ReadAllText("Tracks.txt");
                Tracks = JsonConvert.DeserializeObject<List<Track>>(text);
            }

            if (File.Exists("Albums.txt"))
            {
                var text = File.ReadAllText("Albums.txt");
                Albums = JsonConvert.DeserializeObject<List<Album>>(text);
            }

            if (File.Exists("Playlists.txt"))
            {
                var text = File.ReadAllText("Playlists.txt");
                Playlists = JsonConvert.DeserializeObject<List<Playlist>>(text);
            }
        }

        public void Save()
        {
            var artists = JsonConvert.SerializeObject(Artists);
            File.WriteAllText("Artists.txt", artists);

            var genres = JsonConvert.SerializeObject(Genres);
            File.WriteAllText("Genres.txt", genres);

            var tracks = JsonConvert.SerializeObject(Tracks);
            File.WriteAllText("Tracks.txt", tracks);

            var albums = JsonConvert.SerializeObject(Albums);
            File.WriteAllText("Albums.txt", albums);

            var playlists = JsonConvert.SerializeObject(Playlists);
            File.WriteAllText("Playlists.txt", playlists);
        }

    }
}

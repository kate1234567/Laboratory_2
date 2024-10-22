using CatalogMusic.Entity;
using CatalogMusic.Patterns;

namespace CatalogMusic
{
    public class SearchEngine
    {

        internal List<Track> _tracks;
        internal List<Album> _albums;
        internal List<Playlist> _playlists;

        public BuilderSearch CriteriosSearch;

        public SearchEngine(List<Track> tracks, List<Album> albums, List<Playlist> playlists)
        {
            _tracks = tracks;
            _albums = albums;
            _playlists = playlists;
            CriteriosSearch = new BuilderSearch(tracks);
        }

        public List<Artist> FindArtists(string nickname)
        {
            var list = new List<Artist>();
            var search = _tracks.Where(x => x.Artist.Nickname.ToLower().Contains(nickname.ToLower())).Select(x => new Artist(x.Artist.Nickname)).ToList();
            foreach (var item in search)
            {
                if (!list.Any(x => x.Nickname == item.Nickname))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public List<Album> FindAlbums(string title)
        {
            return _albums.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public List<Playlist> FindPlaylists(string title)
        {
            return _playlists.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public List<Track> FindByCriterio(BuilderSearch criterios)
        {
            return criterios.GetResult();
        }
    }
}

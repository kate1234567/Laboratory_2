using CatalogMusic.Entity;

namespace CatalogMusic.Patterns
{
    public class BuilderSearch
    {
        private List<Track> _tracks;

        public BuilderSearch(List<Track> tracks)
        {
            _tracks = tracks;
        }

        public BuilderSearch SetCriterioNicknameArtist(string nickname)
        {
            _tracks = _tracks.Where(x => x.Artist.Nickname.ToLower().Contains(nickname.ToLower())).ToList();
            return this;
        }

        public BuilderSearch SetCriterioGenre(string genre)
        {
            _tracks = _tracks.Where(x => x.Genre.Title.ToLower().Contains(genre.ToLower())).ToList();
            return this;
        }

        public BuilderSearch SetCriterioTitleTrack(string title)
        {
            _tracks = _tracks.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
            return this;
        }

        public List<Track> GetResult()
        {
            return _tracks;
        }
    }
}

namespace CatalogMusic.Entity
{
    public class Playlist : IAddedTrack
    {
        public string Title { get; set; }
        public List<Track> Tracks { get; set; }

        public Playlist(string title)
        {
            Title = title;
        }

        public override string ToString()
        {
            return $"{Title} - {Tracks.Count} трека(ов)";
        }

        public void AddTrack(Track track)
        {
            if (Tracks == null)
            {
                Tracks = new List<Track>();
            }
            Tracks.Add(track);
        }

        public void RemoveTrack(Track track)
        {
            if (Tracks != null)
            {
                Tracks.Remove(track);
            }
        }
    }
}

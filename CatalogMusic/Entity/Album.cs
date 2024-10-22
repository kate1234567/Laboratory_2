namespace CatalogMusic.Entity
{
    public class Album : IAddedTrack
    {
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public List<Track> Tracks { get; set; }

        public Album(string title, Artist artist)
        {
            Title = title;
            Artist = artist;
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

        public override string ToString()
        {
            return Title;
        }
    }
}

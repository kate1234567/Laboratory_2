namespace CatalogMusic.Entity
{
    public class Track
    {
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }

        public Track(string title, Artist artist, Genre genre)
        {
            Title = title;
            Artist = artist;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{Title} - {Artist.Nickname} ({Genre.Title})";
        }
    }
}

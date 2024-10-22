namespace CatalogMusic.Entity
{
    public class Genre
    {
        public string Title { get; set; }

        public Genre(string title)
        {
            Title = title;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}

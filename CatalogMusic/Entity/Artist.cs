namespace CatalogMusic.Entity
{
    public class Artist
    {
        public string Nickname { get; set; }

        public Artist(string nickname)
        {
            Nickname = nickname;
        }

        public override string ToString()
        {
            return Nickname;
        }
    }
}

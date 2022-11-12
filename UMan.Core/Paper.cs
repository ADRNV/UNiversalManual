namespace UMan.Core
{
    public class Paper
    {
        public int Id { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();

        public List<HashTag> HashTags { get; set; } = new List<HashTag>();

        public DateTime Created { get; set; }

        public Author Author { get; set; }
    }
}

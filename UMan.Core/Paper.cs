namespace UMan.Core
{
    public class Paper
    {
        public List<Article> Articles { get; set; } = new List<Article>();

        public DateTime Created { get; set; }

        public Author? Author { get; set; }
    }
}

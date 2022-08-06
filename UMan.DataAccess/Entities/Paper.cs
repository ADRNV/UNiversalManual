namespace UMan.DataAccess.Entities
{
    public class Paper
    {
        public int Id { get; set; }

        public List<Article> Articles { get; set; } = new List<Article>();

        public DateTime Created { get; set; }

        public Author? Author { get; set; }
    }
}

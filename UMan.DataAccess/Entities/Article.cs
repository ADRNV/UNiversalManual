namespace UMan.DataAccess.Entities
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public List<Paper> Papers { get; set; } = new List<Paper>();
    }
}

namespace UMan.Core
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<Paper> Papers { get; set; }
    }
}

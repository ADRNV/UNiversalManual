namespace UMan.Core.Pagination.Paged
{
    public class AuthorsPage : Page<Author>
    {
        public AuthorsPage(IEnumerable<Author> items, int totalCount, string? error = null) 
            : base(items, totalCount, error)
        {
        }
    }
}

namespace UMan.Core.Pagination.Paged
{
    public class PapersPage : Page<Paper>
    {
        public PapersPage(IEnumerable<Paper> papers, int totalCount, string? error = null)
            : base(papers, totalCount, error)
        {
        }
    }
}

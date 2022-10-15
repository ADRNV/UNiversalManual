using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

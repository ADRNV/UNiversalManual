using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMan.Core.Repositories
{
    public interface IPapersRepository : IRepository<Paper>
    {
        Task<IEnumerable<Paper>> GetByTag(IEnumerable<HashTag> hashTag);
    }
}

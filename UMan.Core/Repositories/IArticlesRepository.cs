using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMan.Core.Repositories
{
    public interface IArticlesRepository<T>
    {
        Task<T[]> Get();

        Task<T> Get(int id);

        Task<bool> Add(T article);

        Task<int> Update(int id, T article);

        Task<int> Delete(int id);                       
    }
}

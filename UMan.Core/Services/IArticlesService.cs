using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMan.Core.Services
{
    public interface IArticlesService<T>
    {
        public Task<T> Get();

        public Task<T> Get(int id);

        public Task Delete(int id);

        public Task Create<T>(T article);
    }
}

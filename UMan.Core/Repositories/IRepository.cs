using UMan.Core.Pagination;

namespace UMan.Core.Repositories
{
    public interface IRepository<T>
    {
        Task<T[]> Get(CancellationToken cancellationToken = default);

        Task<T> Get(int id, CancellationToken cancellationToken = default);

        Task<int> Add(T entity, CancellationToken cancellationToken = default);

        Task<int> Update(int id, T entity, CancellationToken cancellationToken = default);

        Task<bool> Delete(int id, CancellationToken cancellationToken = default);

        Task<Page<T>> Get(QueryParameters queryParameters, CancellationToken cancellationToken = default);
    }
}

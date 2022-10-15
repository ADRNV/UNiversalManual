using UMan.Core.Pagination;

namespace UMan.Core.Repositories
{
    public interface IPagingSource<T>
    {
        public Task<Page<T>> Get(QueryParameters queryParameters, CancellationToken cancellationToken = default);
    }
}

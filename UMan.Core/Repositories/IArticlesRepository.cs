namespace UMan.Core.Repositories
{
    public interface IRepository<T>
    {
        Task<T[]> Get();

        Task<T> Get(int id);

        Task<bool> Add(T article);

        Task<int> Update(int id, T article);

        Task<int> Delete(int id);
    }
}

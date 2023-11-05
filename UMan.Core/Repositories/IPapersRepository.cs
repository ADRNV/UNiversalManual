namespace UMan.Core.Repositories
{
    public interface IPapersRepository : IRepository<Paper>
    {
        Task<IEnumerable<Paper>> GetByTag(IEnumerable<HashTag> hashTag);

        Task Save();
    }
}

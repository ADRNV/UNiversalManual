namespace UMan.DataAccess.Security.Common
{
    public interface IPasswordHasher
    {
        public interface IPasswordHasher : IDisposable
        {
            Task<byte[]> Hash(string password, byte[] salt);
        }
    }
}

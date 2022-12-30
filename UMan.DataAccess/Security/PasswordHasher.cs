using System.Security.Cryptography;
using System.Text;
using UMan.DataAccess.Security.Common;

namespace UMan.DataAccess.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly HMACSHA512 x = new(Encoding.UTF8.GetBytes("umancrud"));

        public Task<byte[]> Hash(string password, byte[] salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            var allBytes = new byte[bytes.Length + salt.Length];
            Buffer.BlockCopy(bytes, 0, allBytes, 0, bytes.Length);
            Buffer.BlockCopy(salt, 0, allBytes, bytes.Length, salt.Length);

            return x.ComputeHashAsync(new MemoryStream(allBytes));
        }

        public void Dispose() => x.Dispose();
    }
}

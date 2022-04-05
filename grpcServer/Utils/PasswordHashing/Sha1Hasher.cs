// using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace grpcServer.Utils.PasswordHashing
{
    public class Sha1Hasher : IPasswordHasher
    {
        private static Sha1Hasher instance = new Sha1Hasher();

        public static Sha1Hasher getInstance()
        {
            return instance;
        }
        public string HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
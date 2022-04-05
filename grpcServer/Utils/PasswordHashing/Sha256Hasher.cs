using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace grpcServer.Utils.PasswordHashing
{
    public class Sha256Hasher: IPasswordHasher
    {
        private static Sha256Hasher instance = new Sha256Hasher();

        public static Sha256Hasher getInstance()
        {
            return instance;
        }
        public string HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
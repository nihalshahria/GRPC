using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace grpcServer.Utils.PasswordHashing
{
    public class Sha512Hasher: IPasswordHasher
    {
        private static Sha512Hasher instance = new Sha512Hasher();

        public static Sha512Hasher getInstance()
        {
            return instance;
        }
        public string HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
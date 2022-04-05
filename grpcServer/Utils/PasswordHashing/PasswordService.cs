using System.Security.Cryptography;
// using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace grpcServer.Utils.PasswordHashing
{
    public class PasswordService
    {
        private IPasswordHasher passwordHasher;
        public byte[] salt{get;set;}
        public PasswordService(IPasswordHasher passwordHasher)
        {
            this.passwordHasher = passwordHasher;
            SaltGenerator();
        }

        public string HashPassword(string password)
        {
            return passwordHasher.HashPassword(password, salt);
        }

        public string HashPassword(string password, byte[] salt)
        {
            return passwordHasher.HashPassword(password, salt);
        }

        private void SaltGenerator()
        {
            salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            string candidatePassword = passwordHasher.HashPassword(password, salt);
            return candidatePassword == hashedPassword;
        }
    }
}
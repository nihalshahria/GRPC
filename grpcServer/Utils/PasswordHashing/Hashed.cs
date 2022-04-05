namespace grpcServer.Utils.PasswordHashing
{
    public abstract class Hashed 
    {
        public IPasswordHasher passwordHasher;
        String hashedPassword;

        public void GenerateHash(string password, byte[] salt)
        {
            hashedPassword = passwordHasher.HashPassword(password, salt);
        }
    }
}
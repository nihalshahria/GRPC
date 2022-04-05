namespace grpcServer.Utils.PasswordHashing
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, byte[] salt);
    }
    
}
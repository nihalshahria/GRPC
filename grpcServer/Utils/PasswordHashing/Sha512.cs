namespace grpcServer.Utils.PasswordHashing
{
    public class Sha512:Hashed
    {
        public Sha512()
        {
            this.passwordHasher = new Sha512Hasher();
        }
    }
}
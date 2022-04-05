namespace grpcServer.Utils.PasswordHashing
{
    public class Sha256:Hashed
    {
        public Sha256()
        {
            this.passwordHasher = new Sha256Hasher();
        }
    }
}
namespace grpcServer.Utils.PasswordHashing
{
    public class Sha1:Hashed
    {
        public Sha1()
        {
            this.passwordHasher = new Sha1Hasher();
        }
    }
}
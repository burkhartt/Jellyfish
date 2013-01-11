namespace Authentication {
    public interface IAuthenticator {
        bool Authenticate(string emailAddress, string password);
    }
}
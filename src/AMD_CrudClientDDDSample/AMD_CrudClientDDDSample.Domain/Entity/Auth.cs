namespace AMD_CrudClientDDDSample.Domain.Entity
{
    public class Auth
    {
        public bool Authenticated { get; private set; }
        public string Created { get; private set; }
        public string Expiration { get; private set; }
        public string AccessToken { get; private set; }
        public string Message { get; private set; }

        public Auth(bool authenticated, string created, string expiration, string accessToken, string message)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            Message = message;
        }

    }
}
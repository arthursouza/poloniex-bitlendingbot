namespace Jojatekok.PoloniexAPI.General
{
    public class Authenticator : IAuthenticator
    {
        public ApiWebClient WebClient { get; }

        internal Authenticator(ApiWebClient apiWebClient, string publicKey, string privateKey) : this(apiWebClient)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
            apiWebClient.Authenticator = this;
        }

        private Authenticator(ApiWebClient apiWebClient)
        {
            WebClient = apiWebClient;
        }

        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
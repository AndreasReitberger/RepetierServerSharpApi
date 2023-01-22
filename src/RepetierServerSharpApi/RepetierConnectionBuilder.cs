namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {
        public class RepetierConnectionBuilder
        {
            #region Instance
            readonly RepetierClient _client = new();
            #endregion

            #region Methods

            public RepetierClient Build()
            {
                return _client;
            }

            public RepetierConnectionBuilder WithServerAddress(string serverAddress, int port = 3344, bool https = false)
            {
                _client.IsSecure = https;
                _client.ServerAddress = serverAddress;
                _client.Port = port;
                return this;
            }

            public RepetierConnectionBuilder WithApiKey(string apiKey)
            {
                _client.ApiKey = apiKey;
                return this;
            }

            #endregion
        }
    }
}

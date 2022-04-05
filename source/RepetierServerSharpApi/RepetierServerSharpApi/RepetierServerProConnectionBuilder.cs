namespace AndreasReitberger
{
    public partial class RepetierServerPro
    {
        public class RepetierServerProConnectionBuilder
        {
            #region Instance
            readonly RepetierServerPro _client = new();
            #endregion

            #region Methods

            public RepetierServerPro Build()
            {
                return _client;
            }

            public RepetierServerProConnectionBuilder WithServerAddress(string serverAddress, int port = 3344, bool https = false)
            {
                _client.IsSecure = https;
                _client.ServerAddress = serverAddress;
                _client.Port = port;
                return this;
            }

            public RepetierServerProConnectionBuilder WithApiKey(string apiKey)
            {
                _client.API = apiKey;
                return this;
            }

            #endregion
        }
    }
}

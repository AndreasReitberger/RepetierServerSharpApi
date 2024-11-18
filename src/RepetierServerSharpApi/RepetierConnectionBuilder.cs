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
                _client.Target = Print3dServer.Core.Enums.Print3dServerTarget.RepetierServer;
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

            public RepetierConnectionBuilder WithName(string name)
            {
                _client.ServerName = name;
                return this;
            }

            public RepetierConnectionBuilder WithPingInterval(int interval = 5)
            {
                _client.PingInterval = interval;
                return this;
            }

            #endregion
        }
    }
}

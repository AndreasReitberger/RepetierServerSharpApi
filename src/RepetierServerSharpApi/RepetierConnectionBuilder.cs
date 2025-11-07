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

            public RepetierConnectionBuilder WithServerAddress(string serverAddress, string version = "")
            {
                _client.ApiTargetPath = serverAddress;
                _client.ApiVersion = version;
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

            public RepetierConnectionBuilder WithPingInterval(bool enablePing, int interval = 5)
            {
                _client.EnablePing = enablePing;
                _client.PingInterval = interval;
                return this;
            }

            public RepetierConnectionBuilder WithTimeout(int timeout = 100)
            {
                _client.DefaultTimeout = timeout;
                return this;
            }
            public RepetierConnectionBuilder WithWebSocket(string websocketTarget, string pingCommand = "", int pingInterval = 5, bool enablePing = true)
            {
                _client.WebSocketTargetUri = websocketTarget;
                _client.PingCommand = pingCommand;
                _client.PingInterval = pingInterval;
                _client.EnablePing = enablePing;
                return this;
            }
            #endregion
        }
    }
}

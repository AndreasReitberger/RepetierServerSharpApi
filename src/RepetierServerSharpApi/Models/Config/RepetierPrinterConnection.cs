using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("compressCommunication"), JsonPropertyName("compressCommunication")]
        public partial bool CompressCommunication { get; set; }

        [ObservableProperty]
        [JsonProperty("connectionMethod"), JsonPropertyName("connectionMethod")]
        public partial long ConnectionMethod { get; set; }

        [ObservableProperty]
        [JsonProperty("continueAfterFastReconnect"), JsonPropertyName("continueAfterFastReconnect")]
        public partial bool ContinueAfterFastReconnect { get; set; }

        [ObservableProperty]
        [JsonProperty("ip"), JsonPropertyName("ip")]
        public partial RepetierPrinterConnectionIp? Ip { get; set; }

        [ObservableProperty]
        [JsonProperty("lcdTimeMode"), JsonPropertyName("lcdTimeMode")]
        public partial long LcdTimeMode { get; set; }

        [ObservableProperty]
        [JsonProperty("password"), JsonPropertyName("password")]
        public partial string Password { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("pipe"), JsonPropertyName("pipe")]
        public partial RepetierPrinterConnectionPipe? Pipe { get; set; }

        [ObservableProperty]
        [JsonProperty("powerOffIdleMinutes"), JsonPropertyName("powerOffIdleMinutes")]
        public partial long PowerOffIdleMinutes { get; set; }

        [ObservableProperty]
        [JsonProperty("powerOffMaxTemperature"), JsonPropertyName("powerOffMaxTemperature")]
        public partial long PowerOffMaxTemperature { get; set; }

        [ObservableProperty]
        [JsonProperty("resetScript"), JsonPropertyName("resetScript")]
        public partial string ResetScript { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("serial"), JsonPropertyName("serial")]
        public partial RepetierPrinterConnectionSerial? Serial { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

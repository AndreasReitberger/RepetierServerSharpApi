using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("compressCommunication")]
        public partial bool CompressCommunication { get; set; }

        [ObservableProperty]

        [JsonProperty("connectionMethod")]
        public partial long ConnectionMethod { get; set; }

        [ObservableProperty]

        [JsonProperty("continueAfterFastReconnect")]
        public partial bool ContinueAfterFastReconnect { get; set; }

        [ObservableProperty]

        [JsonProperty("ip")]
        public partial RepetierPrinterConnectionIp? Ip { get; set; }

        [ObservableProperty]

        [JsonProperty("lcdTimeMode")]
        public partial long LcdTimeMode { get; set; }

        [ObservableProperty]

        [JsonProperty("password")]
        public partial string Password { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("pipe")]
        public partial RepetierPrinterConnectionPipe? Pipe { get; set; }

        [ObservableProperty]

        [JsonProperty("powerOffIdleMinutes")]
        public partial long PowerOffIdleMinutes { get; set; }

        [ObservableProperty]

        [JsonProperty("powerOffMaxTemperature")]
        public partial long PowerOffMaxTemperature { get; set; }

        [ObservableProperty]

        [JsonProperty("resetScript")]
        public partial string ResetScript { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("serial")]
        public partial RepetierPrinterConnectionSerial? Serial { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

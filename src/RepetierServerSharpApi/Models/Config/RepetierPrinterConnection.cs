namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("compressCommunication")]
        public partial bool CompressCommunication { get; set; }

        [ObservableProperty]
        [JsonPropertyName("connectionMethod")]
        public partial long ConnectionMethod { get; set; }

        [ObservableProperty]
        [JsonPropertyName("continueAfterFastReconnect")]
        public partial bool ContinueAfterFastReconnect { get; set; }

        [ObservableProperty]
        [JsonPropertyName("ip")]
        public partial RepetierPrinterConnectionIp? Ip { get; set; }

        [ObservableProperty]
        [JsonPropertyName("lcdTimeMode")]
        public partial long LcdTimeMode { get; set; }

        [ObservableProperty]
        [JsonPropertyName("password")]
        public partial string Password { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("pipe")]
        public partial RepetierPrinterConnectionPipe? Pipe { get; set; }

        [ObservableProperty]
        [JsonPropertyName("powerOffIdleMinutes")]
        public partial long PowerOffIdleMinutes { get; set; }

        [ObservableProperty]
        [JsonPropertyName("powerOffMaxTemperature")]
        public partial long PowerOffMaxTemperature { get; set; }

        [ObservableProperty]
        [JsonPropertyName("resetScript")]
        public partial string ResetScript { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("serial")]
        public partial RepetierPrinterConnectionSerial? Serial { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConnection);

        #endregion
    }
}

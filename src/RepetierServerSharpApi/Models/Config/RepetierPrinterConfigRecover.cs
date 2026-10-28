namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigRecover : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("delayBeforeReconnect")]
        public partial long DelayBeforeReconnect { get; set; }

        [ObservableProperty]
        [JsonPropertyName("enabled")]
        public partial bool Enabled { get; set; }

        [ObservableProperty]
        [JsonPropertyName("extraZOnFirmwareDetect")]
        public partial double ExtraZOnFirmwareDetect { get; set; }

        [ObservableProperty]
        [JsonPropertyName("firmwarePowerlossSignal")]
        public partial string FirmwarePowerlossSignal { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("maxTimeForAutocontinue")]
        public partial long MaxTimeForAutocontinue { get; set; }

        [ObservableProperty]
        [JsonPropertyName("procedure")]
        public partial string Procedure { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("reactivateBedOnConnect")]
        public partial bool ReactivateBedOnConnect { get; set; }

        [ObservableProperty]
        [JsonPropertyName("replayExtruderSwitches")]
        public partial bool ReplayExtruderSwitches { get; set; }

        [ObservableProperty]
        [JsonPropertyName("runOnConnect")]
        public partial string RunOnConnect { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigRecover);

        #endregion
    }
}

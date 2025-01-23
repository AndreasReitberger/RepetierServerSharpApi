using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigRecover : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("delayBeforeReconnect")]
        public partial long DelayBeforeReconnect { get; set; }

        [ObservableProperty]

        [JsonProperty("enabled")]
        public partial bool Enabled { get; set; }

        [ObservableProperty]

        [JsonProperty("extraZOnFirmwareDetect")]
        public partial long ExtraZOnFirmwareDetect { get; set; }

        [ObservableProperty]

        [JsonProperty("firmwarePowerlossSignal")]
        public partial string FirmwarePowerlossSignal { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("maxTimeForAutocontinue")]
        public partial long MaxTimeForAutocontinue { get; set; }

        [ObservableProperty]

        [JsonProperty("procedure")]
        public partial string Procedure { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("reactivateBedOnConnect")]
        public partial bool ReactivateBedOnConnect { get; set; }

        [ObservableProperty]

        [JsonProperty("replayExtruderSwitches")]
        public partial bool ReplayExtruderSwitches { get; set; }

        [ObservableProperty]

        [JsonProperty("runOnConnect")]
        public partial string RunOnConnect { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

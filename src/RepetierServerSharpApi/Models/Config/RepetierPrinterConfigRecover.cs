using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigRecover : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("delayBeforeReconnect"), JsonPropertyName("delayBeforeReconnect")]
        public partial long DelayBeforeReconnect { get; set; }

        [ObservableProperty]
        [JsonProperty("enabled"), JsonPropertyName("enabled")]
        public partial bool Enabled { get; set; }

        [ObservableProperty]
        [JsonProperty("extraZOnFirmwareDetect"), JsonPropertyName("extraZOnFirmwareDetect")]
        public partial double ExtraZOnFirmwareDetect { get; set; }

        [ObservableProperty]
        [JsonProperty("firmwarePowerlossSignal"), JsonPropertyName("firmwarePowerlossSignal")]
        public partial string FirmwarePowerlossSignal { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("maxTimeForAutocontinue"), JsonPropertyName("maxTimeForAutocontinue")]
        public partial long MaxTimeForAutocontinue { get; set; }

        [ObservableProperty]
        [JsonProperty("procedure"), JsonPropertyName("procedure")]
        public partial string Procedure { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("reactivateBedOnConnect"), JsonPropertyName("reactivateBedOnConnect")]
        public partial bool ReactivateBedOnConnect { get; set; }

        [ObservableProperty]
        [JsonProperty("replayExtruderSwitches"), JsonPropertyName("replayExtruderSwitches")]
        public partial bool ReplayExtruderSwitches { get; set; }

        [ObservableProperty]
        [JsonProperty("runOnConnect"), JsonPropertyName("runOnConnect")]
        public partial string RunOnConnect { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

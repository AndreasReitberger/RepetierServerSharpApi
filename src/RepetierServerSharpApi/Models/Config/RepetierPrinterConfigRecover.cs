using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigRecover : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("delayBeforeReconnect")]
        long delayBeforeReconnect;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("enabled")]
        bool enabled;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("extraZOnFirmwareDetect")]
        long extraZOnFirmwareDetect;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("firmwarePowerlossSignal")]
        string firmwarePowerlossSignal = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("maxTimeForAutocontinue")]
        long maxTimeForAutocontinue;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("procedure")]
        string procedure = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("reactivateBedOnConnect")]
        bool reactivateBedOnConnect;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("replayExtruderSwitches")]
        bool replayExtruderSwitches;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("runOnConnect")]
        string runOnConnect = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

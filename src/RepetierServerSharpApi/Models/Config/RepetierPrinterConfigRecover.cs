using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigRecover : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("delayBeforeReconnect")]
        [property: JsonIgnore]
        long delayBeforeReconnect;

        [ObservableProperty]
        [JsonProperty("enabled")]
        [property: JsonIgnore]
        bool enabled;

        [ObservableProperty]
        [JsonProperty("extraZOnFirmwareDetect")]
        [property: JsonIgnore]
        long extraZOnFirmwareDetect;

        [ObservableProperty]
        [JsonProperty("firmwarePowerlossSignal")]
        [property: JsonIgnore]
        string firmwarePowerlossSignal;

        [ObservableProperty]
        [JsonProperty("maxTimeForAutocontinue")]
        [property: JsonIgnore]
        long maxTimeForAutocontinue;

        [ObservableProperty]
        [JsonProperty("procedure")]
        [property: JsonIgnore]
        string procedure;

        [ObservableProperty]
        [JsonProperty("reactivateBedOnConnect")]
        [property: JsonIgnore]
        bool reactivateBedOnConnect;

        [ObservableProperty]
        [JsonProperty("replayExtruderSwitches")]
        [property: JsonIgnore]
        bool replayExtruderSwitches;

        [ObservableProperty]
        [JsonProperty("runOnConnect")]
        [property: JsonIgnore]
        string runOnConnect;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

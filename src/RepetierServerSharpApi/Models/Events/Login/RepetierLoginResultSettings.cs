using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResultSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("gcodeGroup")]
        [property: JsonIgnore]
        string gcodeGroup;

        [ObservableProperty]
        [JsonProperty("gcodeSortBy")]
        [property: JsonIgnore]
        long? gcodeSortBy;

        [ObservableProperty]
        [JsonProperty("gcodeViewMode")]
        [property: JsonIgnore]
        long? gcodeViewMode;

        [ObservableProperty]
        [JsonProperty("tempDiagActive")]
        [property: JsonIgnore]
        long? tempDiagActive;

        [ObservableProperty]
        [JsonProperty("tempDiagAll")]
        [property: JsonIgnore]
        long? tempDiagAll;

        [ObservableProperty]
        [JsonProperty("tempDiagBed")]
        [property: JsonIgnore]
        long? tempDiagBed;

        [ObservableProperty]
        [JsonProperty("tempDiagChamber")]
        [property: JsonIgnore]
        long? tempDiagChamber;

        [ObservableProperty]
        [JsonProperty("tempDiagMode")]
        [property: JsonIgnore]
        long? tempDiagMode;

        [ObservableProperty]
        [JsonProperty("theme")]
        [property: JsonIgnore]
        string theme;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

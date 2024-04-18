using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResultSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gcodeGroup")]
        string gcodeGroup;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gcodeSortBy")]
        long? gcodeSortBy;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gcodeViewMode")]
        long? gcodeViewMode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tempDiagActive")]
        long? tempDiagActive;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tempDiagAll")]
        long? tempDiagAll;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tempDiagBed")]
        long? tempDiagBed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tempDiagChamber")]
        long? tempDiagChamber;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tempDiagMode")]
        long? tempDiagMode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("theme")]
        string theme;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

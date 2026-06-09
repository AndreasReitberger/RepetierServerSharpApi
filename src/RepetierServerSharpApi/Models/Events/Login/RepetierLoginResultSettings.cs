using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResultSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("gcodeGroup"), JsonPropertyName("gcodeGroup")]
        public partial string GcodeGroup { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("gcodeSortBy"), JsonPropertyName("gcodeSortBy")]
        public partial long? GcodeSortBy { get; set; }

        [ObservableProperty]
        [JsonProperty("gcodeViewMode"), JsonPropertyName("gcodeViewMode")]
        public partial long? GcodeViewMode { get; set; }

        [ObservableProperty]
        [JsonProperty("tempDiagActive"), JsonPropertyName("tempDiagActive")]
        public partial long? TempDiagActive { get; set; }

        [ObservableProperty]
        [JsonProperty("tempDiagAll"), JsonPropertyName("tempDiagAll")]
        public partial long? TempDiagAll { get; set; }

        [ObservableProperty]
        [JsonProperty("tempDiagBed"), JsonPropertyName("tempDiagBed")]
        public partial long? TempDiagBed { get; set; }

        [ObservableProperty]
        [JsonProperty("tempDiagChamber"), JsonPropertyName("tempDiagChamber")]
        public partial long? TempDiagChamber { get; set; }

        [ObservableProperty]
        [JsonProperty("tempDiagMode"), JsonPropertyName("tempDiagMode")]
        public partial long? TempDiagMode { get; set; }

        [ObservableProperty]
        [JsonProperty("theme"), JsonPropertyName("theme")]
        public partial string Theme { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

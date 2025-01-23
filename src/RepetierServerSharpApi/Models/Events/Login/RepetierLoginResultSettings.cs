using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResultSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("gcodeGroup")]
        public partial string GcodeGroup { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("gcodeSortBy")]
        public partial long? GcodeSortBy { get; set; }

        [ObservableProperty]

        [JsonProperty("gcodeViewMode")]
        public partial long? GcodeViewMode { get; set; }

        [ObservableProperty]

        [JsonProperty("tempDiagActive")]
        public partial long? TempDiagActive { get; set; }

        [ObservableProperty]

        [JsonProperty("tempDiagAll")]
        public partial long? TempDiagAll { get; set; }

        [ObservableProperty]

        [JsonProperty("tempDiagBed")]
        public partial long? TempDiagBed { get; set; }

        [ObservableProperty]

        [JsonProperty("tempDiagChamber")]
        public partial long? TempDiagChamber { get; set; }

        [ObservableProperty]

        [JsonProperty("tempDiagMode")]
        public partial long? TempDiagMode { get; set; }

        [ObservableProperty]

        [JsonProperty("theme")]
        public partial string Theme { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

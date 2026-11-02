namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResultSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("gcodeGroup")]
        public partial string GcodeGroup { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("gcodeSortBy")]
        public partial long? GcodeSortBy { get; set; }

        [ObservableProperty]
        [JsonPropertyName("gcodeViewMode")]
        public partial long? GcodeViewMode { get; set; }

        [ObservableProperty]
        [JsonPropertyName("tempDiagActive")]
        public partial long? TempDiagActive { get; set; }

        [ObservableProperty]
        [JsonPropertyName("tempDiagAll")]
        public partial long? TempDiagAll { get; set; }

        [ObservableProperty]
        [JsonPropertyName("tempDiagBed")]
        public partial long? TempDiagBed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("tempDiagChamber")]
        public partial long? TempDiagChamber { get; set; }

        [ObservableProperty]
        [JsonPropertyName("tempDiagMode")]
        public partial long? TempDiagMode { get; set; }

        [ObservableProperty]
        [JsonPropertyName("theme")]
        public partial string Theme { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierLoginResultSettings);
        #endregion
    }
}

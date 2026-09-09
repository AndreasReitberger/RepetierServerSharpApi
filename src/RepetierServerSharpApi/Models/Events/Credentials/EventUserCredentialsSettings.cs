namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("gcodeGroup")]
        public partial string GcodeGroup { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("gcodeSortBy")]
        public partial long GcodeSortBy { get; set; }

        [ObservableProperty]
        [JsonPropertyName("gcodeViewMode")]
        public partial long GcodeViewMode { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventUserCredentialsSettings);
        #endregion
    }
}

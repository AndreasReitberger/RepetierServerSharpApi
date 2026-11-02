namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTemp : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial EventTempData? Data { get; set; }

        [ObservableProperty]
        [JsonPropertyName("event")]
        public partial string EventName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("printer")]
        public partial string Printer { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventTemp);
        #endregion
    }
}

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTimer : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial object? Data { get; set; }

        [ObservableProperty]
        [JsonPropertyName("event")]
        public partial string? EventName { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventTimer);
        #endregion
    }
}

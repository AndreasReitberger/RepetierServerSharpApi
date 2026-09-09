namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobFinishedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("duration")]
        public partial long? Duration { get; set; }

        [ObservableProperty]
        [JsonPropertyName("end")]
        public partial long? End { get; set; }

        [ObservableProperty]
        [JsonPropertyName("lines")]
        public partial long? Lines { get; set; }

        [ObservableProperty]
        [JsonPropertyName("start")]
        public partial long? Start { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventJobFinishedData);
        #endregion
    }
}

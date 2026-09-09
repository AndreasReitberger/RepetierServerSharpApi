namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectComment : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("comment")]
        public partial string Comment { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("time")]
        public partial long? Time { get; set; }

        [ObservableProperty]
        [JsonPropertyName("user")]
        public partial string User { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierProjectsProjectComment);
        #endregion
    }
}

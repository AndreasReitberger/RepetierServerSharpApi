namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectFile : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("n")]
        public partial string N { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("s")]
        public partial long? S { get; set; }

        [ObservableProperty]
        [JsonPropertyName("p")]
        public partial string P { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierProjectsProjectFile);
        #endregion
    }
}

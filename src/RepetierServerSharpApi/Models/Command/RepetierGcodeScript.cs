namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGcodeScript : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonPropertyName("script")]
        public partial string Script { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierGcodeScript);
        #endregion
    }
}

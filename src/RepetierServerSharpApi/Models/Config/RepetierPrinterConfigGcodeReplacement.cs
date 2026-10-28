namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGcodeReplacement : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("comment")]
        public partial string Comment { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("expression")]
        public partial string Expression { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("script")]
        public partial string Script { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigGcodeReplacement);

        #endregion
    }
}

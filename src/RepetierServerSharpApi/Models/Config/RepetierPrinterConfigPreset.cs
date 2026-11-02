namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigPreset : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("value")]
        public partial int Value { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigPreset);

        #endregion
    }
}

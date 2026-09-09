namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("online")]
        public partial long Online { get; set; }

        [ObservableProperty]
        [JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterInfo);
        #endregion
    }
}

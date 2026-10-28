namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigHeatedComponent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("alias")]
        public partial string Alias { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("cooldownPerSecond")]
        public partial double? CooldownPerSecond { get; set; }

        [ObservableProperty]
        [JsonPropertyName("heatupPerSecond")]
        public partial double? HeatupPerSecond { get; set; }

        [ObservableProperty]
        [JsonPropertyName("lastTemp")]
        public partial double? LastTemp { get; set; }

        [ObservableProperty]
        [JsonPropertyName("maxTemp")]
        public partial double? MaxTemp { get; set; }

        [ObservableProperty]
        [JsonPropertyName("offset")]
        public partial double? Offset { get; set; }

        [ObservableProperty]
        [JsonPropertyName("temperatures")]
        public partial List<RepetierPrinterConfigTemperature> Temperatures { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigHeatedComponent);

        #endregion
    }
}

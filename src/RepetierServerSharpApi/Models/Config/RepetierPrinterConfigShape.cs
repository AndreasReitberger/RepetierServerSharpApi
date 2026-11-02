namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigShape : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("basicShape")]
        public partial RepetierPrinterConfigBasicShape? BasicShape { get; set; }

        [ObservableProperty]
        [JsonPropertyName("gridColor")]
        public partial string GridColor { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("gridSpacing")]
        public partial double GridSpacing { get; set; }

        [ObservableProperty]
        [JsonPropertyName("imageExtension")]
        public partial string ImageExtension { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("imageOpacity")]
        public partial double ImageOpacity { get; set; }

        [ObservableProperty]
        [JsonPropertyName("imageZoom")]
        public partial double ImageZoom { get; set; }

        [ObservableProperty]
        [JsonPropertyName("marker")]
        public partial List<object> Marker { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("showImage")]
        public partial bool ShowImage { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigShape);

        #endregion
    }
}

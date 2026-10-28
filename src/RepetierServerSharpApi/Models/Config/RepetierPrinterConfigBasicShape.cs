namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigBasicShape : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("angle")]
        public partial double Angle { get; set; }

        [ObservableProperty]
        [JsonPropertyName("color")]
        public partial string Color { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("radius")]
        public partial double Radius { get; set; }

        [ObservableProperty]
        [JsonPropertyName("shape")]
        public partial string Shape { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("x")]
        public partial double X { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMax")]
        public partial double XMax { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMin")]
        public partial double XMin { get; set; }

        [ObservableProperty]
        [JsonPropertyName("y")]
        public partial double Y { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMax")]
        public partial double YMax { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMin")]
        public partial double YMin { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigBasicShape);

        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigBasicShape : ObservableObject
    {
        #region Properties

        [ObservableProperty]

        [JsonProperty("angle")]
        public partial long Angle { get; set; }

        [ObservableProperty]

        [JsonProperty("color")]
        public partial string Color { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("radius")]
        public partial long Radius { get; set; }

        [ObservableProperty]

        [JsonProperty("shape")]
        public partial string Shape { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("x")]
        public partial long X { get; set; }

        [ObservableProperty]

        [JsonProperty("xMax")]
        public partial long XMax { get; set; }

        [ObservableProperty]

        [JsonProperty("xMin")]
        public partial long XMin { get; set; }

        [ObservableProperty]

        [JsonProperty("y")]
        public partial long Y { get; set; }

        [ObservableProperty]

        [JsonProperty("yMax")]
        public partial long YMax { get; set; }

        [ObservableProperty]

        [JsonProperty("yMin")]
        public partial long YMin { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

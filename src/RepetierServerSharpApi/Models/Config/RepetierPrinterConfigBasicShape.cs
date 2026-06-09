using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigBasicShape : ObservableObject
    {
        #region Properties

        [ObservableProperty]

        [JsonProperty("angle"), JsonPropertyName("angle")]
        public partial long Angle { get; set; }

        [ObservableProperty]

        [JsonProperty("color"), JsonPropertyName("color")]
        public partial string Color { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("radius"), JsonPropertyName("radius")]
        public partial long Radius { get; set; }

        [ObservableProperty]

        [JsonProperty("shape"), JsonPropertyName("shape")]
        public partial string Shape { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("x"), JsonPropertyName("x")]
        public partial long X { get; set; }

        [ObservableProperty]

        [JsonProperty("xMax"), JsonPropertyName("xMax")]
        public partial long XMax { get; set; }

        [ObservableProperty]

        [JsonProperty("xMin"), JsonPropertyName("xMin")]
        public partial long XMin { get; set; }

        [ObservableProperty]

        [JsonProperty("y"), JsonPropertyName("y")]
        public partial long Y { get; set; }

        [ObservableProperty]

        [JsonProperty("yMax"), JsonPropertyName("yMax")]
        public partial long YMax { get; set; }

        [ObservableProperty]

        [JsonProperty("yMin"), JsonPropertyName("yMin")]
        public partial long YMin { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

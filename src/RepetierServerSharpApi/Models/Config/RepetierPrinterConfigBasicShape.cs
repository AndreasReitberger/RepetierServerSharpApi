using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigBasicShape : ObservableObject
    {
        #region Properties

        [ObservableProperty]

        [JsonProperty("angle"), JsonPropertyName("angle")]
        public partial double Angle { get; set; }

        [ObservableProperty]

        [JsonProperty("color"), JsonPropertyName("color")]
        public partial string Color { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("radius"), JsonPropertyName("radius")]
        public partial double Radius { get; set; }

        [ObservableProperty]

        [JsonProperty("shape"), JsonPropertyName("shape")]
        public partial string Shape { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("x"), JsonPropertyName("x")]
        public partial double X { get; set; }

        [ObservableProperty]

        [JsonProperty("xMax"), JsonPropertyName("xMax")]
        public partial double XMax { get; set; }

        [ObservableProperty]

        [JsonProperty("xMin"), JsonPropertyName("xMin")]
        public partial double XMin { get; set; }

        [ObservableProperty]

        [JsonProperty("y"), JsonPropertyName("y")]
        public partial double Y { get; set; }

        [ObservableProperty]

        [JsonProperty("yMax"), JsonPropertyName("yMax")]
        public partial double YMax { get; set; }

        [ObservableProperty]

        [JsonProperty("yMin"), JsonPropertyName("yMin")]
        public partial double YMin { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

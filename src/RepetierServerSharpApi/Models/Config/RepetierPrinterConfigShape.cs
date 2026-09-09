using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigShape : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("basicShape"), JsonPropertyName("basicShape")]
        public partial RepetierPrinterConfigBasicShape? BasicShape { get; set; }

        [ObservableProperty]
        [JsonProperty("gridColor"), JsonPropertyName("gridColor")]
        public partial string GridColor { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("gridSpacing"), JsonPropertyName("gridSpacing")]
        public partial double GridSpacing { get; set; }

        [ObservableProperty]
        [JsonProperty("imageExtension"), JsonPropertyName("imageExtension")]
        public partial string ImageExtension { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("imageOpacity"), JsonPropertyName("imageOpacity")]
        public partial double ImageOpacity { get; set; }

        [ObservableProperty]
        [JsonProperty("imageZoom"), JsonPropertyName("imageZoom")]
        public partial double ImageZoom { get; set; }

        [ObservableProperty]
        [JsonProperty("marker"), JsonPropertyName("marker")]
        public partial List<object> Marker { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("showImage"), JsonPropertyName("showImage")]
        public partial bool ShowImage { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

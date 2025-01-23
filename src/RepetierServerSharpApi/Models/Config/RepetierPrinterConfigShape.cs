using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigShape : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("basicShape")]
        public partial RepetierPrinterConfigBasicShape? BasicShape { get; set; }

        [ObservableProperty]

        [JsonProperty("gridColor")]
        public partial string GridColor { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("gridSpacing")]
        public partial long GridSpacing { get; set; }

        [ObservableProperty]

        [JsonProperty("imageExtension")]
        public partial string ImageExtension { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("imageOpacity")]
        public partial long ImageOpacity { get; set; }

        [ObservableProperty]

        [JsonProperty("imageZoom")]
        public partial long ImageZoom { get; set; }

        [ObservableProperty]

        [JsonProperty("marker")]
        public partial List<object> Marker { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("showImage")]
        public partial bool ShowImage { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

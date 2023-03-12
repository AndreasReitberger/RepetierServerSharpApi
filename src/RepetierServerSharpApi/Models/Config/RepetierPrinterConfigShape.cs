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
        RepetierPrinterConfigBasicShape basicShape;

        [ObservableProperty]
        [JsonProperty("gridColor")]
        string gridColor;

        [ObservableProperty]
        [JsonProperty("gridSpacing")]
        long gridSpacing;

        [ObservableProperty]
        [JsonProperty("imageExtension")]
        string imageExtension;

        [ObservableProperty]
        [JsonProperty("imageOpacity")]
        long imageOpacity;

        [ObservableProperty]
        [JsonProperty("imageZoom")]
        long imageZoom;

        [ObservableProperty]
        [JsonProperty("marker")]
        List<object> marker = new();

        [ObservableProperty]
        [JsonProperty("showImage")]
        bool showImage;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

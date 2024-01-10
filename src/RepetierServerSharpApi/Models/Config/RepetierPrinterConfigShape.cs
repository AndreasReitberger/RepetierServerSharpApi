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
        [property: JsonIgnore]
        RepetierPrinterConfigBasicShape basicShape;

        [ObservableProperty]
        [JsonProperty("gridColor")]
        [property: JsonIgnore]
        string gridColor;

        [ObservableProperty]
        [JsonProperty("gridSpacing")]
        [property: JsonIgnore]
        long gridSpacing;

        [ObservableProperty]
        [JsonProperty("imageExtension")]
        [property: JsonIgnore]
        string imageExtension;

        [ObservableProperty]
        [JsonProperty("imageOpacity")]
        [property: JsonIgnore]
        long imageOpacity;

        [ObservableProperty]
        [JsonProperty("imageZoom")]
        [property: JsonIgnore]
        long imageZoom;

        [ObservableProperty]
        [JsonProperty("marker")]
        [property: JsonIgnore]
        List<object> marker = new();

        [ObservableProperty]
        [JsonProperty("showImage")]
        [property: JsonIgnore]
        bool showImage;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

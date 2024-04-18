using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigShape : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("basicShape")]
        RepetierPrinterConfigBasicShape? basicShape;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gridColor")]
        string gridColor = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gridSpacing")]
        long gridSpacing;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("imageExtension")]
        string imageExtension = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("imageOpacity")]
        long imageOpacity;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("imageZoom")]
        long imageZoom;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("marker")]
        List<object> marker = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("showImage")]
        bool showImage;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

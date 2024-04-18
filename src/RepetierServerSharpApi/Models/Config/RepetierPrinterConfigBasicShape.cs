using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigBasicShape : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("angle")]
        long angle;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("color")]
        string color = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("radius")]
        long radius;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("shape")]
        string shape = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("x")]
        long x;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMax")]
        long xMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMin")]
        long xMin;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("y")]
        long y;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMax")]
        long yMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMin")]
        long yMin;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigBasicShape : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("angle")]
        [property: JsonIgnore]
        long angle;

        [ObservableProperty]
        [JsonProperty("color")]
        [property: JsonIgnore]
        string color;

        [ObservableProperty]
        [JsonProperty("radius")]
        [property: JsonIgnore]
        long radius;

        [ObservableProperty]
        [JsonProperty("shape")]
        [property: JsonIgnore]
        string shape;

        [ObservableProperty]
        [JsonProperty("x")]
        [property: JsonIgnore]
        long x;

        [ObservableProperty]
        [JsonProperty("xMax")]
        [property: JsonIgnore]
        long xMax;

        [ObservableProperty]
        [JsonProperty("xMin")]
        [property: JsonIgnore]
        long xMin;

        [ObservableProperty]
        [JsonProperty("y")]
        [property: JsonIgnore]
        long y;

        [ObservableProperty]
        [JsonProperty("yMax")]
        [property: JsonIgnore]
        long yMax;

        [ObservableProperty]
        [JsonProperty("yMin")]
        [property: JsonIgnore]
        long yMin;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

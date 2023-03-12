using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigBasicShape : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("angle")]
        long angle;

        [ObservableProperty]
        [JsonProperty("color")]
        string color;

        [ObservableProperty]
        [JsonProperty("radius")]
        long radius;

        [ObservableProperty]
        [JsonProperty("shape")]
        string shape;

        [ObservableProperty]
        [JsonProperty("x")]
        long x;

        [ObservableProperty]
        [JsonProperty("xMax")]
        long xMax;

        [ObservableProperty]
        [JsonProperty("xMin")]
        long xMin;

        [ObservableProperty]
        [JsonProperty("y")]
        long y;

        [ObservableProperty]
        [JsonProperty("yMax")]
        long yMax;

        [ObservableProperty]
        [JsonProperty("yMin")]
        long yMin;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

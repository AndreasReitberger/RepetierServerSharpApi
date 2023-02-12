using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigHeatedComponent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("alias")]
        string alias;

        [ObservableProperty]
        [JsonProperty("cooldownPerSecond")]
        double? cooldownPerSecond;

        [ObservableProperty]
        [JsonProperty("heatupPerSecond")]
        double? heatupPerSecond;

        [ObservableProperty]
        [JsonProperty("lastTemp")]
        long? lastTemp;

        [ObservableProperty]
        [JsonProperty("maxTemp")]
        long? maxTemp;

        [ObservableProperty]
        [JsonProperty("offset")]
        long? offset;

        [ObservableProperty]
        [JsonProperty("temperatures")]
        List<RepetierPrinterConfigTemperature> temperatures = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

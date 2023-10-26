using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigHeatedComponent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("alias")]
        [property: JsonIgnore]
        string alias;

        [ObservableProperty]
        [JsonProperty("cooldownPerSecond")]
        [property: JsonIgnore]
        double? cooldownPerSecond;

        [ObservableProperty]
        [JsonProperty("heatupPerSecond")]
        [property: JsonIgnore]
        double? heatupPerSecond;

        [ObservableProperty]
        [JsonProperty("lastTemp")]
        [property: JsonIgnore]
        long? lastTemp;

        [ObservableProperty]
        [JsonProperty("maxTemp")]
        [property: JsonIgnore]
        long? maxTemp;

        [ObservableProperty]
        [JsonProperty("offset")]
        [property: JsonIgnore]
        long? offset;

        [ObservableProperty]
        [JsonProperty("temperatures")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigTemperature> temperatures = new();
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

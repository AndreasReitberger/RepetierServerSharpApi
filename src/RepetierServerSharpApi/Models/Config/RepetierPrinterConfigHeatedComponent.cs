using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigHeatedComponent : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("alias")]
        string alias = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("cooldownPerSecond")]
        double? cooldownPerSecond;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("heatupPerSecond")]
        double? heatupPerSecond;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("lastTemp")]
        long? lastTemp;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("maxTemp")]
        long? maxTemp;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("offset")]
        long? offset;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("temperatures")]
        List<RepetierPrinterConfigTemperature> temperatures = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

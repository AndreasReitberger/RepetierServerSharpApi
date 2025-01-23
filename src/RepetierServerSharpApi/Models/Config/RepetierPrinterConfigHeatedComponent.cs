using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigHeatedComponent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("alias")]
        public partial string Alias { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("cooldownPerSecond")]
        public partial double? CooldownPerSecond { get; set; }

        [ObservableProperty]
        
        [JsonProperty("heatupPerSecond")]
        public partial double? HeatupPerSecond { get; set; }

        [ObservableProperty]
        
        [JsonProperty("lastTemp")]
        public partial long? LastTemp { get; set; }

        [ObservableProperty]
        
        [JsonProperty("maxTemp")]
        public partial long? MaxTemp { get; set; }

        [ObservableProperty]
        
        [JsonProperty("offset")]
        public partial long? Offset { get; set; }

        [ObservableProperty]
        
        [JsonProperty("temperatures")]
        public partial List<RepetierPrinterConfigTemperature> Temperatures { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

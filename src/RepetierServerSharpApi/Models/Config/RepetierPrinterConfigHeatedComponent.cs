using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigHeatedComponent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("alias"), JsonPropertyName("alias")]
        public partial string Alias { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("cooldownPerSecond"), JsonPropertyName("cooldownPerSecond")]
        public partial double? CooldownPerSecond { get; set; }

        [ObservableProperty]
        [JsonProperty("heatupPerSecond"), JsonPropertyName("heatupPerSecond")]
        public partial double? HeatupPerSecond { get; set; }

        [ObservableProperty]
        [JsonProperty("lastTemp"), JsonPropertyName("lastTemp")]
        public partial double? LastTemp { get; set; }

        [ObservableProperty]
        [JsonProperty("maxTemp"), JsonPropertyName("maxTemp")]
        public partial double? MaxTemp { get; set; }

        [ObservableProperty]
        [JsonProperty("offset"), JsonPropertyName("offset")]
        public partial double? Offset { get; set; }

        [ObservableProperty]
        [JsonProperty("temperatures"), JsonPropertyName("temperatures")]
        public partial List<RepetierPrinterConfigTemperature> Temperatures { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

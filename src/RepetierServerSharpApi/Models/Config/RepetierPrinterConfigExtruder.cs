using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigExtruder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("acceleration"), JsonPropertyName("acceleration")]
        public partial double? Acceleration { get; set; }

        [ObservableProperty]
        [JsonProperty("alias"), JsonPropertyName("alias")]
        public partial string Alias { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("changeFastDistance"), JsonPropertyName("changeFastDistance")]
        public partial double? ChangeFastDistance { get; set; }

        [ObservableProperty]
        [JsonProperty("changeSlowDistance"), JsonPropertyName("changeSlowDistance")]
        public partial double? ChangeSlowDistance { get; set; }

        [ObservableProperty]
        [JsonProperty("cooldownPerSecond"), JsonPropertyName("cooldownPerSecond")]
        public partial double? CooldownPerSecond { get; set; }

        [ObservableProperty]
        [JsonProperty("eJerk"), JsonPropertyName("eJerk")]
        public partial double? EJerk { get; set; }

        [ObservableProperty]
        [JsonProperty("extrudeSpeed"), JsonPropertyName("extrudeSpeed")]
        public partial double? ExtrudeSpeed { get; set; }

        [ObservableProperty]
        [JsonProperty("filamentDiameter"), JsonPropertyName("filamentDiameter")]
        public partial double? FilamentDiameter { get; set; }

        [ObservableProperty]
        [JsonProperty("heatupPerSecond"), JsonPropertyName("heatupPerSecond")]
        public partial double? HeatupPerSecond { get; set; }

        [ObservableProperty]
        [JsonProperty("lastTemp"), JsonPropertyName("lastTemp")]
        public partial double? LastTemp { get; set; }

        [ObservableProperty]
        [JsonProperty("maxSpeed"), JsonPropertyName("maxSpeed")]
        public partial double? MaxSpeed { get; set; }

        [ObservableProperty]
        [JsonProperty("maxTemp"), JsonPropertyName("maxTemp")]
        public partial double? MaxTemp { get; set; }

        [ObservableProperty]
        [JsonProperty("num"), JsonPropertyName("num")]
        public partial long? Num { get; set; }

        [ObservableProperty]
        [JsonProperty("offset"), JsonPropertyName("offset")]
        public partial double? Offset { get; set; }

        [ObservableProperty]
        [JsonProperty("offsetX"), JsonPropertyName("offsetX")]
        public partial double? OffsetX { get; set; }

        [ObservableProperty]
        [JsonProperty("offsetY"), JsonPropertyName("offsetY")]
        public partial double? OffsetY { get; set; }

        [ObservableProperty]
        [JsonProperty("retractSpeed"), JsonPropertyName("retractSpeed")]
        public partial double? RetractSpeed { get; set; }

        [ObservableProperty]
        [JsonProperty("supportTemperature"), JsonPropertyName("supportTemperature")]
        public partial bool? SupportTemperature { get; set; }

        [ObservableProperty]
        [JsonProperty("tempMaster"), JsonPropertyName("tempMaster")]
        public partial long? TempMaster { get; set; }

        [ObservableProperty]
        [JsonProperty("temperatures"), JsonPropertyName("temperatures")]
        public partial List<RepetierPrinterConfigTemperature> Temperatures { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("toolDiameter"), JsonPropertyName("toolDiameter")]
        public partial double? ToolDiameter { get; set; }

        [ObservableProperty]
        [JsonProperty("toolType"), JsonPropertyName("toolType")]
        public partial long? ToolType { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

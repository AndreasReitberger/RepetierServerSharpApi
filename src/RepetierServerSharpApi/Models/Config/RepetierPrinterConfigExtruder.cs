using Newtonsoft.Json;
using System.Collections.Generic;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigExtruder : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("acceleration")]
        public partial long? Acceleration { get; set; }

        [ObservableProperty]

        [JsonProperty("alias")]
        public partial string Alias { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("changeFastDistance")]
        public partial long? ChangeFastDistance { get; set; }

        [ObservableProperty]

        [JsonProperty("changeSlowDistance")]
        public partial long? ChangeSlowDistance { get; set; }

        [ObservableProperty]

        [JsonProperty("cooldownPerSecond")]
        public partial double? CooldownPerSecond { get; set; }

        [ObservableProperty]

        [JsonProperty("eJerk")]
        public partial long? EJerk { get; set; }

        [ObservableProperty]

        [JsonProperty("extrudeSpeed")]
        public partial long? ExtrudeSpeed { get; set; }

        [ObservableProperty]

        [JsonProperty("filamentDiameter")]
        public partial double? FilamentDiameter { get; set; }

        [ObservableProperty]

        [JsonProperty("heatupPerSecond")]
        public partial long? HeatupPerSecond { get; set; }

        [ObservableProperty]

        [JsonProperty("lastTemp")]
        public partial long? LastTemp { get; set; }

        [ObservableProperty]

        [JsonProperty("maxSpeed")]
        public partial long? MaxSpeed { get; set; }

        [ObservableProperty]

        [JsonProperty("maxTemp")]
        public partial long? MaxTemp { get; set; }

        [ObservableProperty]

        [JsonProperty("num")]
        public partial long? Num { get; set; }

        [ObservableProperty]

        [JsonProperty("offset")]
        public partial long? Offset { get; set; }

        [ObservableProperty]

        [JsonProperty("offsetX")]
        public partial long? OffsetX { get; set; }

        [ObservableProperty]

        [JsonProperty("offsetY")]
        public partial long? OffsetY { get; set; }

        [ObservableProperty]

        [JsonProperty("retractSpeed")]
        public partial long? RetractSpeed { get; set; }

        [ObservableProperty]

        [JsonProperty("supportTemperature")]
        public partial bool? SupportTemperature { get; set; }

        [ObservableProperty]

        [JsonProperty("tempMaster")]
        public partial long? TempMaster { get; set; }

        [ObservableProperty]

        [JsonProperty("temperatures")]
        public partial List<RepetierPrinterConfigTemperature> Temperatures { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("toolDiameter")]
        public partial double? ToolDiameter { get; set; }

        [ObservableProperty]

        [JsonProperty("toolType")]
        public partial long? ToolType { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigExtruder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("acceleration")]
        public partial double? Acceleration { get; set; }

        [ObservableProperty]
        [JsonPropertyName("alias")]
        public partial string Alias { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("changeFastDistance")]
        public partial double? ChangeFastDistance { get; set; }

        [ObservableProperty]
        [JsonPropertyName("changeSlowDistance")]
        public partial double? ChangeSlowDistance { get; set; }

        [ObservableProperty]
        [JsonPropertyName("cooldownPerSecond")]
        public partial double? CooldownPerSecond { get; set; }

        [ObservableProperty]
        [JsonPropertyName("eJerk")]
        public partial double? EJerk { get; set; }

        [ObservableProperty]
        [JsonPropertyName("extrudeSpeed")]
        public partial double? ExtrudeSpeed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("filamentDiameter")]
        public partial double? FilamentDiameter { get; set; }

        [ObservableProperty]
        [JsonPropertyName("heatupPerSecond")]
        public partial double? HeatupPerSecond { get; set; }

        [ObservableProperty]
        [JsonPropertyName("lastTemp")]
        public partial double? LastTemp { get; set; }

        [ObservableProperty]
        [JsonPropertyName("maxSpeed")]
        public partial double? MaxSpeed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("maxTemp")]
        public partial double? MaxTemp { get; set; }

        [ObservableProperty]
        [JsonPropertyName("num")]
        public partial long? Num { get; set; }

        [ObservableProperty]
        [JsonPropertyName("offset")]
        public partial double? Offset { get; set; }

        [ObservableProperty]
        [JsonPropertyName("offsetX")]
        public partial double? OffsetX { get; set; }

        [ObservableProperty]
        [JsonPropertyName("offsetY")]
        public partial double? OffsetY { get; set; }

        [ObservableProperty]
        [JsonPropertyName("retractSpeed")]
        public partial double? RetractSpeed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("supportTemperature")]
        public partial bool? SupportTemperature { get; set; }

        [ObservableProperty]
        [JsonPropertyName("tempMaster")]
        public partial long? TempMaster { get; set; }

        [ObservableProperty]
        [JsonPropertyName("temperatures")]
        public partial List<RepetierPrinterConfigTemperature> Temperatures { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("toolDiameter")]
        public partial double? ToolDiameter { get; set; }

        [ObservableProperty]
        [JsonPropertyName("toolType")]
        public partial long? ToolType { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigExtruder);

        #endregion
    }
}

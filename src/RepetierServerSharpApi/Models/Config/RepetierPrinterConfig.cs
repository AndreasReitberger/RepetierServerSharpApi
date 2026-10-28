namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfig : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("buttonCommands")]
        public partial List<RepetierPrinterConfigButtonCommand> ButtonCommands { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("connection")]
        public partial RepetierPrinterConnection? Connection { get; set; }

        [ObservableProperty]
        [JsonPropertyName("extruders")]
        public partial List<RepetierPrinterConfigExtruder> Extruders { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("fanPresets")]
        public partial List<RepetierPrinterConfigPreset> FanPresets { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("flowPresets")]
        public partial List<RepetierPrinterConfigPreset> FlowPresets { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("gcodeReplacements")]
        public partial List<RepetierPrinterConfigGcodeReplacement> GcodeReplacements { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("general")]
        public partial RepetierPrinterConfigGeneral? General { get; set; }

        [ObservableProperty]
        [JsonPropertyName("heatedBeds")]
        public partial List<RepetierPrinterConfigHeatedComponent> HeatedBeds { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("heatedChambers")]
        public partial List<RepetierPrinterConfigHeatedComponent> HeatedChambers { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("movement")]
        public partial RepetierPrinterConfigMovement? Movement { get; set; }

        [ObservableProperty]
        [JsonPropertyName("properties")]
        public partial RepetierPrinterConfigProperties? Properties { get; set; }

        [ObservableProperty]
        [JsonPropertyName("quickCommands")]
        public partial List<RepetierQuickGcodeCommand> QuickCommands { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("recover")]
        public partial RepetierPrinterConfigRecover? Recover { get; set; }

        [ObservableProperty]
        [JsonPropertyName("responseEvents")]
        public partial List<object> ResponseEvents { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("shape")]
        public partial RepetierPrinterConfigShape? Shape { get; set; }

        [ObservableProperty]
        [JsonPropertyName("speedPresets")]
        public partial List<RepetierPrinterConfigPreset> SpeedPresets { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("webcams")]
        public partial List<RepetierPrinterConfigWebcam> Webcams { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("wizardCommands")]
        public partial List<object> WizardCommands { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("wizardTemplates")]
        public partial List<RepetierPrinterConfigWizardTemplate> WizardTemplates { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfig);

        #endregion
    }
}

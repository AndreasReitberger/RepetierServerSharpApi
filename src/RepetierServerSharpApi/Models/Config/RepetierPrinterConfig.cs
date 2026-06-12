using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfig : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("buttonCommands", NullValueHandling = NullValueHandling.Ignore), JsonPropertyName("buttonCommands")]
        public partial List<RepetierPrinterConfigButtonCommand> ButtonCommands { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("connection"), JsonPropertyName("connection")]
        public partial RepetierPrinterConnection? Connection { get; set; }

        [ObservableProperty]
        [JsonProperty("extruders"), JsonPropertyName("extruders")]
        public partial List<RepetierPrinterConfigExtruder> Extruders { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("fanPresets"), JsonPropertyName("fanPresets")]
        public partial List<RepetierPrinterConfigPreset> FanPresets { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("flowPresets"), JsonPropertyName("flowPresets")]
        public partial List<RepetierPrinterConfigPreset> FlowPresets { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("gcodeReplacements"), JsonPropertyName("gcodeReplacements")]
        public partial List<RepetierPrinterConfigGcodeReplacement> GcodeReplacements { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("general"), JsonPropertyName("general")]
        public partial RepetierPrinterConfigGeneral? General { get; set; }

        [ObservableProperty]
        [JsonProperty("heatedBeds"), JsonPropertyName("heatedBeds")]
        public partial List<RepetierPrinterConfigHeatedComponent> HeatedBeds { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("heatedChambers"), JsonPropertyName("heatedChambers")]
        public partial List<RepetierPrinterConfigHeatedComponent> HeatedChambers { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("movement"), JsonPropertyName("movement")]
        public partial RepetierPrinterConfigMovement? Movement { get; set; }

        [ObservableProperty]
        [JsonProperty("properties"), JsonPropertyName("properties")]
        public partial RepetierPrinterConfigProperties? Properties { get; set; }

        [ObservableProperty]
        [JsonProperty("quickCommands"), JsonPropertyName("quickCommands")]
        public partial List<RepetierQuickGcodeCommand> QuickCommands { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("recover"), JsonPropertyName("recover")]
        public partial RepetierPrinterConfigRecover? Recover { get; set; }

        [ObservableProperty]
        [JsonProperty("responseEvents"), JsonPropertyName("responseEvents")]
        public partial List<object> ResponseEvents { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("shape"), JsonPropertyName("shape")]
        public partial RepetierPrinterConfigShape? Shape { get; set; }

        [ObservableProperty]
        [JsonProperty("speedPresets"), JsonPropertyName("speedPresets")]
        public partial List<RepetierPrinterConfigPreset> SpeedPresets { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("webcams"), JsonPropertyName("webcams")]
        public partial List<RepetierPrinterConfigWebcam> Webcams { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("wizardCommands"), JsonPropertyName("wizardCommands")]
        public partial List<object> WizardCommands { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("wizardTemplates"), JsonPropertyName("wizardTemplates")]
        public partial List<RepetierPrinterConfigWizardTemplate> WizardTemplates { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

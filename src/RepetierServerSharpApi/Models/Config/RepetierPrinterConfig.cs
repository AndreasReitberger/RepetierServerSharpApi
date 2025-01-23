using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfig : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("buttonCommands", NullValueHandling = NullValueHandling.Ignore)]
        public partial List<RepetierPrinterConfigButtonCommand> ButtonCommands { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("connection")]
        public partial RepetierPrinterConnection? Connection { get; set; }

        [ObservableProperty]

        [JsonProperty("extruders")]
        public partial List<RepetierPrinterConfigExtruder> Extruders { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("fanPresets")]
        public partial List<RepetierPrinterConfigPreset> FanPresets { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("flowPresets")]
        public partial List<RepetierPrinterConfigPreset> FlowPresets { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("gcodeReplacements")]
        public partial List<RepetierPrinterConfigGcodeReplacement> GcodeReplacements { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("general")]
        public partial RepetierPrinterConfigGeneral? General { get; set; }

        [ObservableProperty]

        [JsonProperty("heatedBeds")]
        public partial List<RepetierPrinterConfigHeatedComponent> HeatedBeds { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("heatedChambers")]
        public partial List<RepetierPrinterConfigHeatedComponent> HeatedChambers { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("movement")]
        public partial RepetierPrinterConfigMovement? Movement { get; set; }

        [ObservableProperty]

        [JsonProperty("properties")]
        public partial RepetierPrinterConfigProperties? Properties { get; set; }

        [ObservableProperty]

        [JsonProperty("quickCommands")]
        public partial List<RepetierQuickGcodeCommand> QuickCommands { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("recover")]
        public partial RepetierPrinterConfigRecover? Recover { get; set; }

        [ObservableProperty]

        [JsonProperty("responseEvents")]
        public partial List<object> ResponseEvents { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("shape")]
        public partial RepetierPrinterConfigShape? Shape { get; set; }

        [ObservableProperty]

        [JsonProperty("speedPresets")]
        public partial List<RepetierPrinterConfigPreset> SpeedPresets { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("webcams")]
        public partial List<RepetierPrinterConfigWebcam> Webcams { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("wizardCommands")]
        public partial List<object> WizardCommands { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("wizardTemplates")]
        public partial List<RepetierPrinterConfigWizardTemplate> WizardTemplates { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

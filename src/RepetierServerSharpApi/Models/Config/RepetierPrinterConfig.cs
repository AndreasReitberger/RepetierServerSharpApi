using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfig : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("buttonCommands", NullValueHandling = NullValueHandling.Ignore)]
        List<RepetierPrinterConfigButtonCommand> buttonCommands = new();

        [ObservableProperty]
        [JsonProperty("connection")]
        RepetierPrinterConnection connection;

        [ObservableProperty]
        [JsonProperty("extruders")]
        List<RepetierPrinterConfigExtruder> extruders = new();
        
        [ObservableProperty]
        [JsonProperty("fanPresets")]
        List<RepetierPrinterConfigPreset> fanPresets = new();

        [ObservableProperty]
        [JsonProperty("flowPresets")]
        List<RepetierPrinterConfigPreset> flowPresets = new();

        [ObservableProperty]
        [JsonProperty("gcodeReplacements")]
        List<RepetierPrinterConfigGcodeReplacement> gcodeReplacements = new();

        [ObservableProperty]
        [JsonProperty("general")]
        RepetierPrinterConfigGeneral general;

        [ObservableProperty]
        [JsonProperty("heatedBeds")]
        List<RepetierPrinterConfigHeatedComponent> heatedBeds = new();

        [ObservableProperty]
        [JsonProperty("heatedChambers")]
        List<RepetierPrinterConfigHeatedComponent> heatedChambers = new();

        [ObservableProperty]
        [JsonProperty("movement")]
        RepetierPrinterConfigMovement movement;

        [ObservableProperty]
        [JsonProperty("properties")]
        RepetierPrinterConfigProperties properties;

        [ObservableProperty]
        [JsonProperty("quickCommands")]
        List<RepetierQuickGcodeCommand> quickCommands = new();

        [ObservableProperty]
        [JsonProperty("recover")]
        RepetierPrinterConfigRecover recover;

        [ObservableProperty]
        [JsonProperty("responseEvents")]
        List<object> responseEvents = new();

        [ObservableProperty]
        [JsonProperty("shape")]
        RepetierPrinterConfigShape shape;
        
        [ObservableProperty]
        [JsonProperty("shape")]
        List<RepetierPrinterConfigPreset> speedPresets = new();

        [ObservableProperty]
        [JsonProperty("webcams")]
        List<RepetierPrinterConfigWebcam> webcams = new();

        [ObservableProperty]
        [JsonProperty("wizardCommands")]
        List<object> wizardCommands = new();

        [ObservableProperty]
        [JsonProperty("wizardTemplates")]
        List<RepetierPrinterConfigWizardTemplate> wizardTemplates = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

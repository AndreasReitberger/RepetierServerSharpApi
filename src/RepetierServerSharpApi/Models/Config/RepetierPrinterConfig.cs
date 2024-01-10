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
        [property: JsonIgnore]
        List<RepetierPrinterConfigButtonCommand> buttonCommands = new();

        [ObservableProperty]
        [JsonProperty("connection")]
        [property: JsonIgnore]
        RepetierPrinterConnection connection;

        [ObservableProperty]
        [JsonProperty("extruders")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigExtruder> extruders = new();
        
        [ObservableProperty]
        [JsonProperty("fanPresets")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigPreset> fanPresets = new();

        [ObservableProperty]
        [JsonProperty("flowPresets")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigPreset> flowPresets = new();

        [ObservableProperty]
        [JsonProperty("gcodeReplacements")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigGcodeReplacement> gcodeReplacements = new();

        [ObservableProperty]
        [JsonProperty("general")]
        [property: JsonIgnore]
        RepetierPrinterConfigGeneral general;

        [ObservableProperty]
        [JsonProperty("heatedBeds")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigHeatedComponent> heatedBeds = new();

        [ObservableProperty]
        [JsonProperty("heatedChambers")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigHeatedComponent> heatedChambers = new();

        [ObservableProperty]
        [JsonProperty("movement")]
        [property: JsonIgnore]
        RepetierPrinterConfigMovement movement;

        [ObservableProperty]
        [JsonProperty("properties")]
        [property: JsonIgnore]
        RepetierPrinterConfigProperties properties;

        [ObservableProperty]
        [JsonProperty("quickCommands")]
        [property: JsonIgnore]
        List<RepetierQuickGcodeCommand> quickCommands = new();

        [ObservableProperty]
        [JsonProperty("recover")]
        [property: JsonIgnore]
        RepetierPrinterConfigRecover recover;

        [ObservableProperty]
        [JsonProperty("responseEvents")]
        [property: JsonIgnore]
        List<object> responseEvents = new();

        [ObservableProperty]
        [JsonProperty("shape")]
        [property: JsonIgnore]
        RepetierPrinterConfigShape shape;
        
        [ObservableProperty]
        [JsonProperty("speedPresets")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigPreset> speedPresets = new();

        [ObservableProperty]
        [JsonProperty("webcams")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigWebcam> webcams = new();

        [ObservableProperty]
        [JsonProperty("wizardCommands")]
        [property: JsonIgnore]
        List<object> wizardCommands = new();

        [ObservableProperty]
        [JsonProperty("wizardTemplates")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigWizardTemplate> wizardTemplates = new();
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

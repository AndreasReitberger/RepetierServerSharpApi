using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfig : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("buttonCommands", NullValueHandling = NullValueHandling.Ignore)]
        List<RepetierPrinterConfigButtonCommand> buttonCommands = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("connection")]
        RepetierPrinterConnection connection;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("extruders")]
        List<RepetierPrinterConfigExtruder> extruders = new();
        
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("fanPresets")]
        List<RepetierPrinterConfigPreset> fanPresets = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("flowPresets")]
        List<RepetierPrinterConfigPreset> flowPresets = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gcodeReplacements")]
        List<RepetierPrinterConfigGcodeReplacement> gcodeReplacements = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("general")]
        RepetierPrinterConfigGeneral general;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("heatedBeds")]
        List<RepetierPrinterConfigHeatedComponent> heatedBeds = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("heatedChambers")]
        List<RepetierPrinterConfigHeatedComponent> heatedChambers = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("movement")]
        RepetierPrinterConfigMovement movement;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("properties")]
        RepetierPrinterConfigProperties properties;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("quickCommands")]
        List<RepetierQuickGcodeCommand> quickCommands = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("recover")]
        RepetierPrinterConfigRecover recover;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("responseEvents")]
        List<object> responseEvents = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("shape")]
        RepetierPrinterConfigShape shape;
        
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("speedPresets")]
        List<RepetierPrinterConfigPreset> speedPresets = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("webcams")]
        List<RepetierPrinterConfigWebcam> webcams = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("wizardCommands")]
        List<object> wizardCommands = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("wizardTemplates")]
        List<RepetierPrinterConfigWizardTemplate> wizardTemplates = new();
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

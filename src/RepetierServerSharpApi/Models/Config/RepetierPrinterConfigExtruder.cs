using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigExtruder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("acceleration")]
        long? acceleration;

        [ObservableProperty]
        [JsonProperty("alias")]
        string alias;

        [ObservableProperty]
        [JsonProperty("changeFastDistance")]
        long? changeFastDistance;

        [ObservableProperty]
        [JsonProperty("changeSlowDistance")]
        long? changeSlowDistance;

        [ObservableProperty]
        [JsonProperty("cooldownPerSecond")]
        double? cooldownPerSecond;

        [ObservableProperty]
        [JsonProperty("eJerk")]
        long? eJerk;

        [ObservableProperty]
        [JsonProperty("extrudeSpeed")]
        long? extrudeSpeed;

        [ObservableProperty]
        [JsonProperty("filamentDiameter")]
        double? filamentDiameter;

        [ObservableProperty]
        [JsonProperty("heatupPerSecond")]
        long? heatupPerSecond;

        [ObservableProperty]
        [JsonProperty("lastTemp")]
        long? lastTemp;

        [ObservableProperty]
        [JsonProperty("maxSpeed")]
        long? maxSpeed;

        [ObservableProperty]
        [JsonProperty("maxTemp")]
        long? maxTemp;

        [ObservableProperty]
        [JsonProperty("num")]
        long? num;

        [ObservableProperty]
        [JsonProperty("offset")]
        long? offset;

        [ObservableProperty]
        [JsonProperty("offsetX")]
        long? offsetX;

        [ObservableProperty]
        [JsonProperty("offsetY")]
        long? offsetY;

        [ObservableProperty]
        [JsonProperty("retractSpeed")]
        long? retractSpeed;

        [ObservableProperty]
        [JsonProperty("supportTemperature")]
        bool? supportTemperature;

        [ObservableProperty]
        [JsonProperty("tempMaster")]
        long? tempMaster;

        [ObservableProperty]
        [JsonProperty("temperatures")]
        List<RepetierPrinterConfigTemperature> temperatures;

        [ObservableProperty]
        [JsonProperty("toolDiameter")]
        double? toolDiameter;

        [ObservableProperty]
        [JsonProperty("toolType")]
        long? toolType;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

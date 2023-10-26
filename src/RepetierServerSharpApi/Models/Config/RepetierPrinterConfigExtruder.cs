using Newtonsoft.Json;
using System.Collections.Generic;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigExtruder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("acceleration")]
        [property: JsonIgnore]
        long? acceleration;

        [ObservableProperty]
        [JsonProperty("alias")]
        [property: JsonIgnore]
        string alias;

        [ObservableProperty]
        [JsonProperty("changeFastDistance")]
        [property: JsonIgnore]
        long? changeFastDistance;

        [ObservableProperty]
        [JsonProperty("changeSlowDistance")]
        [property: JsonIgnore]
        long? changeSlowDistance;

        [ObservableProperty]
        [JsonProperty("cooldownPerSecond")]
        [property: JsonIgnore]
        double? cooldownPerSecond;

        [ObservableProperty]
        [JsonProperty("eJerk")]
        [property: JsonIgnore]
        long? eJerk;

        [ObservableProperty]
        [JsonProperty("extrudeSpeed")]
        [property: JsonIgnore]
        long? extrudeSpeed;

        [ObservableProperty]
        [JsonProperty("filamentDiameter")]
        [property: JsonIgnore]
        double? filamentDiameter;

        [ObservableProperty]
        [JsonProperty("heatupPerSecond")]
        [property: JsonIgnore]
        long? heatupPerSecond;

        [ObservableProperty]
        [JsonProperty("lastTemp")]
        [property: JsonIgnore]
        long? lastTemp;

        [ObservableProperty]
        [JsonProperty("maxSpeed")]
        [property: JsonIgnore]
        long? maxSpeed;

        [ObservableProperty]
        [JsonProperty("maxTemp")]
        [property: JsonIgnore]
        long? maxTemp;

        [ObservableProperty]
        [JsonProperty("num")]
        [property: JsonIgnore]
        long? num;

        [ObservableProperty]
        [JsonProperty("offset")]
        [property: JsonIgnore]
        long? offset;

        [ObservableProperty]
        [JsonProperty("offsetX")]
        [property: JsonIgnore]
        long? offsetX;

        [ObservableProperty]
        [JsonProperty("offsetY")]
        [property: JsonIgnore]
        long? offsetY;

        [ObservableProperty]
        [JsonProperty("retractSpeed")]
        [property: JsonIgnore]
        long? retractSpeed;

        [ObservableProperty]
        [JsonProperty("supportTemperature")]
        [property: JsonIgnore]
        bool? supportTemperature;

        [ObservableProperty]
        [JsonProperty("tempMaster")]
        [property: JsonIgnore]
        long? tempMaster;

        [ObservableProperty]
        [JsonProperty("temperatures")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigTemperature> temperatures;

        [ObservableProperty]
        [JsonProperty("toolDiameter")]
        [property: JsonIgnore]
        double? toolDiameter;

        [ObservableProperty]
        [JsonProperty("toolType")]
        [property: JsonIgnore]
        long? toolType;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

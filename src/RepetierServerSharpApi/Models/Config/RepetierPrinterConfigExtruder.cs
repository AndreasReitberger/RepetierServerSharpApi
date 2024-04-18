using Newtonsoft.Json;
using System.Collections.Generic;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigExtruder : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("acceleration")]
        long? acceleration;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("alias")]
        string alias = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("changeFastDistance")]
        long? changeFastDistance;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("changeSlowDistance")]
        long? changeSlowDistance;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("cooldownPerSecond")]
        double? cooldownPerSecond;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("eJerk")]
        long? eJerk;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("extrudeSpeed")]
        long? extrudeSpeed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("filamentDiameter")]
        double? filamentDiameter;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("heatupPerSecond")]
        long? heatupPerSecond;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("lastTemp")]
        long? lastTemp;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("maxSpeed")]
        long? maxSpeed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("maxTemp")]
        long? maxTemp;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("num")]
        long? num;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("offset")]
        long? offset;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("offsetX")]
        long? offsetX;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("offsetY")]
        long? offsetY;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("retractSpeed")]
        long? retractSpeed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("supportTemperature")]
        bool? supportTemperature;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tempMaster")]
        long? tempMaster;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("temperatures")]
        List<RepetierPrinterConfigTemperature> temperatures = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("toolDiameter")]
        double? toolDiameter;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("toolType")]
        long? toolType;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

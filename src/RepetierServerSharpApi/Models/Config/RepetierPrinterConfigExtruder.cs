using Newtonsoft.Json;
using System.Collections.Generic;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigExtruder
    {
        #region Properties
        [JsonProperty("acceleration", NullValueHandling = NullValueHandling.Ignore)]
        public long? Acceleration { get; set; }

        [JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }

        [JsonProperty("changeFastDistance", NullValueHandling = NullValueHandling.Ignore)]
        public long? ChangeFastDistance { get; set; }

        [JsonProperty("changeSlowDistance", NullValueHandling = NullValueHandling.Ignore)]
        public long? ChangeSlowDistance { get; set; }

        [JsonProperty("cooldownPerSecond", NullValueHandling = NullValueHandling.Ignore)]
        public double? CooldownPerSecond { get; set; }

        [JsonProperty("eJerk", NullValueHandling = NullValueHandling.Ignore)]
        public long? EJerk { get; set; }

        [JsonProperty("extrudeSpeed", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExtrudeSpeed { get; set; }

        [JsonProperty("filamentDiameter", NullValueHandling = NullValueHandling.Ignore)]
        public double? FilamentDiameter { get; set; }

        [JsonProperty("heatupPerSecond", NullValueHandling = NullValueHandling.Ignore)]
        public long? HeatupPerSecond { get; set; }

        [JsonProperty("lastTemp", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastTemp { get; set; }

        [JsonProperty("maxSpeed", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxSpeed { get; set; }

        [JsonProperty("maxTemp", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxTemp { get; set; }

        [JsonProperty("num", NullValueHandling = NullValueHandling.Ignore)]
        public long? Num { get; set; }

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public long? Offset { get; set; }

        [JsonProperty("offsetX", NullValueHandling = NullValueHandling.Ignore)]
        public long? OffsetX { get; set; }

        [JsonProperty("offsetY", NullValueHandling = NullValueHandling.Ignore)]
        public long? OffsetY { get; set; }

        [JsonProperty("retractSpeed", NullValueHandling = NullValueHandling.Ignore)]
        public long? RetractSpeed { get; set; }

        [JsonProperty("supportTemperature", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SupportTemperature { get; set; }

        [JsonProperty("tempMaster", NullValueHandling = NullValueHandling.Ignore)]
        public long? TempMaster { get; set; }

        [JsonProperty("temperatures", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierPrinterConfigTemperature> Temperatures { get; set; }

        [JsonProperty("toolDiameter", NullValueHandling = NullValueHandling.Ignore)]
        public double? ToolDiameter { get; set; }

        [JsonProperty("toolType", NullValueHandling = NullValueHandling.Ignore)]
        public long? ToolType { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

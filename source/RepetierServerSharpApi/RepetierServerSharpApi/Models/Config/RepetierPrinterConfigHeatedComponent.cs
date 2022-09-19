using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigHeatedComponent
    {
        #region Properties
        [JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }

        [JsonProperty("cooldownPerSecond", NullValueHandling = NullValueHandling.Ignore)]
        public double? CooldownPerSecond { get; set; }

        [JsonProperty("heatupPerSecond", NullValueHandling = NullValueHandling.Ignore)]
        public double? HeatupPerSecond { get; set; }

        [JsonProperty("lastTemp", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastTemp { get; set; }

        [JsonProperty("maxTemp", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxTemp { get; set; }

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public long? Offset { get; set; }

        [JsonProperty("temperatures", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierPrinterConfigTemperature> Temperatures { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

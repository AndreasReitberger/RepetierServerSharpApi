using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConfigShape
    {
        #region Properties
        [JsonProperty("basicShape")]
        public RepetierPrinterConfigBasicShape BasicShape { get; set; }

        [JsonProperty("gridColor")]
        public string GridColor { get; set; }

        [JsonProperty("gridSpacing")]
        public long GridSpacing { get; set; }

        [JsonProperty("marker")]
        public List<object> Marker { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

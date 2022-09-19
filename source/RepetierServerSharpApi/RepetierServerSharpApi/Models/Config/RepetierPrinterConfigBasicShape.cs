using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigBasicShape
    {
        #region Properties
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("radius")]
        public long Radius { get; set; }

        [JsonProperty("shape")]
        public string Shape { get; set; }

        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("xMax")]
        public long XMax { get; set; }

        [JsonProperty("xMin")]
        public long XMin { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }

        [JsonProperty("yMax")]
        public long YMax { get; set; }

        [JsonProperty("yMin")]
        public long YMin { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

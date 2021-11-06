using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConfigTemperature
    {
        #region Properties
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("temp")]
        public long Temp { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

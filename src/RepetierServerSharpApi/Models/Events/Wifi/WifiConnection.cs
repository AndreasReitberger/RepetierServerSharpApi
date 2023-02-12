using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class WifiConnection
    {
        #region Properties
        [JsonProperty("SSID", NullValueHandling = NullValueHandling.Ignore)]
        public string Ssid { get; set; }

        [JsonProperty("device", NullValueHandling = NullValueHandling.Ignore)]
        public string Device { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

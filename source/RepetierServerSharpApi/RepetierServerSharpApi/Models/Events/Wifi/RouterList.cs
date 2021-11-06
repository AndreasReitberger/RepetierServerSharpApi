using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RouterList
    {
        #region Properties
        [JsonProperty("SSID", NullValueHandling = NullValueHandling.Ignore)]
        public string Ssid { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty("bars", NullValueHandling = NullValueHandling.Ignore)]
        public long? Bars { get; set; }

        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public long? Channel { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public ConnectionData Data { get; set; }

        [JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
        public string Mode { get; set; }

        [JsonProperty("rate", NullValueHandling = NullValueHandling.Ignore)]
        public string Rate { get; set; }

        [JsonProperty("secure", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Secure { get; set; }

        [JsonProperty("signal", NullValueHandling = NullValueHandling.Ignore)]
        public long? Signal { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class EventNetworkInfoRouterList
    {
        #region Properties
        [JsonProperty("SSID")]
        public string Ssid { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("bars")]
        public long Bars { get; set; }

        [JsonProperty("channel")]
        public long Channel { get; set; }

        [JsonProperty("data")]
        public EventNetworkInfoRouterListData Data { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("secure")]
        public bool Secure { get; set; }

        [JsonProperty("signal")]
        public long Signal { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

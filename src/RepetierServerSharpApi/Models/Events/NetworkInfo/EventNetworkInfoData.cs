using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoData
    {
        #region Properties
        [JsonProperty("activeRouter", NullValueHandling = NullValueHandling.Ignore)]
        public bool ActiveRouter { get; set; }

        [JsonProperty("activeSSID", NullValueHandling = NullValueHandling.Ignore)]
        public string ActiveSsid { get; set; }

        [JsonProperty("apMode", NullValueHandling = NullValueHandling.Ignore)]
        public long ApMode { get; set; }

        [JsonProperty("apSSID", NullValueHandling = NullValueHandling.Ignore)]
        public string ApSsid { get; set; }

        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public long Channel { get; set; }

        [JsonProperty("channels", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> Channels { get; set; } = new();

        [JsonProperty("connections", NullValueHandling = NullValueHandling.Ignore)]
        public List<EventNetworkInfoConnection> Connections { get; set; } = new();

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("hostname", NullValueHandling = NullValueHandling.Ignore)]
        public string Hostname { get; set; }

        [JsonProperty("manageable", NullValueHandling = NullValueHandling.Ignore)]
        public bool Manageable { get; set; }

        [JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
        public long Mode { get; set; }

        [JsonProperty("routerList", NullValueHandling = NullValueHandling.Ignore)]
        public List<EventNetworkInfoRouterList> RouterList { get; set; } = new();

        [JsonProperty("screensaver", NullValueHandling = NullValueHandling.Ignore)]
        public bool Screensaver { get; set; }

        [JsonProperty("timezone", NullValueHandling = NullValueHandling.Ignore)]
        public string Timezone { get; set; }

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

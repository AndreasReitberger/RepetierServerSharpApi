using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoRouterListData
    {
        #region Properties
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("ignore")]
        public bool Ignore { get; set; }

        [JsonProperty("ipv4Address")]
        public string Ipv4Address { get; set; }

        [JsonProperty("ipv4Gateway")]
        public string Ipv4Gateway { get; set; }

        [JsonProperty("ipv4MaskBits")]
        public long Ipv4MaskBits { get; set; }

        [JsonProperty("ipv4Mode")]
        public string Ipv4Mode { get; set; }

        [JsonProperty("ipv4Nameserver")]
        public string Ipv4Nameserver { get; set; }

        [JsonProperty("ipv6Address")]
        public string Ipv6Address { get; set; }

        [JsonProperty("ipv6Gateway")]
        public string Ipv6Gateway { get; set; }

        [JsonProperty("ipv6MaskBits")]
        public long Ipv6MaskBits { get; set; }

        [JsonProperty("ipv6Mode")]
        public string Ipv6Mode { get; set; }

        [JsonProperty("ipv6Nameserver")]
        public string Ipv6Nameserver { get; set; }

        [JsonProperty("manualManaged")]
        public bool ManualManaged { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("passwordMethod")]
        public string PasswordMethod { get; set; }

        [JsonProperty("ssid")]
        public string Ssid { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

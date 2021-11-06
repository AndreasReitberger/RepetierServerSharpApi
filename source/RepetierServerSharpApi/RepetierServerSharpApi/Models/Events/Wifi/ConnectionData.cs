using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class ConnectionData
    {
        #region Properties
        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }

        [JsonProperty("hidden", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Hidden { get; set; }

        [JsonProperty("ignore", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Ignore { get; set; }

        [JsonProperty("ipv4Address", NullValueHandling = NullValueHandling.Ignore)]
        public string Ipv4Address { get; set; }

        [JsonProperty("ipv4Gateway", NullValueHandling = NullValueHandling.Ignore)]
        public string Ipv4Gateway { get; set; }

        [JsonProperty("ipv4MaskBits", NullValueHandling = NullValueHandling.Ignore)]
        public long? Ipv4MaskBits { get; set; }

        [JsonProperty("ipv4Mode", NullValueHandling = NullValueHandling.Ignore)]
        public string Ipv4Mode { get; set; }

        [JsonProperty("ipv4Nameserver", NullValueHandling = NullValueHandling.Ignore)]
        public string Ipv4Nameserver { get; set; }

        [JsonProperty("ipv6Address", NullValueHandling = NullValueHandling.Ignore)]
        public string Ipv6Address { get; set; }

        [JsonProperty("ipv6Gateway", NullValueHandling = NullValueHandling.Ignore)]
        public string Ipv6Gateway { get; set; }

        [JsonProperty("ipv6MaskBits", NullValueHandling = NullValueHandling.Ignore)]
        public long? Ipv6MaskBits { get; set; }

        [JsonProperty("ipv6Mode", NullValueHandling = NullValueHandling.Ignore)]
        public string Ipv6Mode { get; set; }

        [JsonProperty("ipv6Nameserver", NullValueHandling = NullValueHandling.Ignore)]
        public string Ipv6Nameserver { get; set; }

        [JsonProperty("manualManaged", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ManualManaged { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty("passwordMethod", NullValueHandling = NullValueHandling.Ignore)]
        public string PasswordMethod { get; set; }

        [JsonProperty("ssid", NullValueHandling = NullValueHandling.Ignore)]
        public string Ssid { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

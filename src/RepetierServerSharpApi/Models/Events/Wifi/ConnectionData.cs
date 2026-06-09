using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ConnectionData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("active"), JsonPropertyName("active")]
        public partial bool? Active { get; set; }

        [ObservableProperty]
        [JsonProperty("hidden"), JsonPropertyName("hidden")]
        public partial bool? Hidden { get; set; }

        [ObservableProperty]
        [JsonProperty("ignore"), JsonPropertyName("ignore")]
        public partial bool? Ignore { get; set; }

        [ObservableProperty]
        [JsonProperty("ipv4Address"), JsonPropertyName("ipv4Address")]
        public partial string Ipv4Address { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("ipv4Gateway"), JsonPropertyName("ipv4Gateway")]
        public partial string Ipv4Gateway { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("ipv4MaskBits"), JsonPropertyName("ipv4MaskBits")]
        public partial long? Ipv4MaskBits { get; set; }

        [ObservableProperty]
        [JsonProperty("ipv4Mode"), JsonPropertyName("ipv4Mode")]
        public partial string Ipv4Mode { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("ipv4Nameserver"), JsonPropertyName("ipv4Nameserver")]
        public partial string Ipv4Nameserver { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("ipv6Address"), JsonPropertyName("ipv6Address")]
        public partial string Ipv6Address { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("ipv6Gateway"), JsonPropertyName("ipv6Gateway")]
        public partial string Ipv6Gateway { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("ipv6MaskBits"), JsonPropertyName("ipv6MaskBits")]
        public partial long? Ipv6MaskBits { get; set; }

        [ObservableProperty]
        [JsonProperty("ipv6Mode"), JsonPropertyName("ipv6Mode")]
        public partial string Ipv6Mode { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("ipv6Nameserver"), JsonPropertyName("ipv6Nameserver")]
        public partial string Ipv6Nameserver { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("manualManaged"), JsonPropertyName("manualManaged")]
        public partial bool? ManualManaged { get; set; }

        [ObservableProperty]
        [JsonProperty("password"), JsonPropertyName("password")]
        public partial string Password { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("passwordMethod"), JsonPropertyName("passwordMethod")]
        public partial string PasswordMethod { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("ssid"), JsonPropertyName("ssid")]
        public partial string Ssid { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

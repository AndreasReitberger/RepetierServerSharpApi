using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoRouterListData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        
        [JsonProperty("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        
        [JsonProperty("hidden")]
        public partial bool Hidden { get; set; }

        [ObservableProperty]
        
        [JsonProperty("ignore")]
        public partial bool Ignore { get; set; }

        [ObservableProperty]
        
        [JsonProperty("ipv4Address")]
        public partial string Ipv4Address { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv4Gateway")]
        public partial string Ipv4Gateway { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv4MaskBits")]
        public partial long Ipv4MaskBits { get; set; }

        [ObservableProperty]
        
        [JsonProperty("ipv4Mode")]
        public partial string Ipv4Mode { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv4Nameserver")]
        public partial string Ipv4Nameserver { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv6Address")]
        public partial string Ipv6Address { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv6Gateway")]
        public partial string Ipv6Gateway { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv6MaskBits")]
        public partial long Ipv6MaskBits { get; set; }

        [ObservableProperty]
        
        [JsonProperty("ipv6Mode")]
        public partial string Ipv6Mode { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv6Nameserver")]
        public partial string Ipv6Nameserver { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("manualManaged")]
        public partial bool ManualManaged { get; set; }

        [ObservableProperty]
        
        [JsonProperty("password")]
        public partial string Password { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("passwordMethod")]
        public partial string PasswordMethod { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ssid")]
        public partial string Ssid { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

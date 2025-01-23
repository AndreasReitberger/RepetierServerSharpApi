using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EthernetConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("ipv4_addresses")]
        public partial string Ipv4Addresses { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv4_dns")]
        public partial string Ipv4Dns { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv4_gateway")]
        public partial string Ipv4Gateway { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv4_method")]
        public partial string Ipv4Method { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv6_addresses")]
        public partial string Ipv6Addresses { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv6_dns")]
        public partial string Ipv6Dns { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv6_gateway")]
        public partial string Ipv6Gateway { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("ipv6_method")]
        public partial string Ipv6Method { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

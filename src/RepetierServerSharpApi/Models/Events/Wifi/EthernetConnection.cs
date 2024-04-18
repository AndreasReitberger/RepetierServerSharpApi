using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EthernetConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv4_addresses")]

        string ipv4Addresses = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv4_dns")]

        string ipv4Dns = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv4_gateway")]

        string ipv4Gateway = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv4_method")]

        string ipv4Method = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv6_addresses")]

        string ipv6Addresses = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv6_dns")]

        string ipv6Dns = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv6_gateway")]

        string ipv6Gateway = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv6_method")]

        string ipv6Method = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EthernetConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("ipv4_addresses")]
        [property: JsonIgnore]
        string ipv4Addresses;

        [ObservableProperty]
        [JsonProperty("ipv4_dns")]
        [property: JsonIgnore]
        string ipv4Dns;

        [ObservableProperty]
        [JsonProperty("ipv4_gateway")]
        [property: JsonIgnore]
        string ipv4Gateway;

        [ObservableProperty]
        [JsonProperty("ipv4_method")]
        [property: JsonIgnore]
        string ipv4Method;

        [ObservableProperty]
        [JsonProperty("ipv6_addresses")]
        [property: JsonIgnore]
        string ipv6Addresses;

        [ObservableProperty]
        [JsonProperty("ipv6_dns")]
        [property: JsonIgnore]
        string ipv6Dns;

        [ObservableProperty]
        [JsonProperty("ipv6_gateway")]
        [property: JsonIgnore]
        string ipv6Gateway;

        [ObservableProperty]
        [JsonProperty("ipv6_method")]
        [property: JsonIgnore]
        string ipv6Method;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

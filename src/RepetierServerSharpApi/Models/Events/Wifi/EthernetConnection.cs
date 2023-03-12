using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EthernetConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("ipv4_addresses")]
        string ipv4Addresses;

        [ObservableProperty]
        [JsonProperty("ipv4_dns")]
        string ipv4Dns;

        [ObservableProperty]
        [JsonProperty("ipv4_gateway")]
        string ipv4Gateway;

        [ObservableProperty]
        [JsonProperty("ipv4_method")]
        string ipv4Method;

        [ObservableProperty]
        [JsonProperty("ipv6_addresses")]
        string ipv6Addresses;

        [ObservableProperty]
        [JsonProperty("ipv6_dns")]
        string ipv6Dns;

        [ObservableProperty]
        [JsonProperty("ipv6_gateway")]
        string ipv6Gateway;

        [ObservableProperty]
        [JsonProperty("ipv6_method")]
        string ipv6Method;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

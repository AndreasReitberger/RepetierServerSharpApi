using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoRouterListData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("active")]
        bool active;

        [ObservableProperty]
        [JsonProperty("hidden")]
        bool hidden;

        [ObservableProperty]
        [JsonProperty("ignore")]
        bool ignore;

        [ObservableProperty]
        [JsonProperty("ipv4Address")]
        string ipv4Address;

        [ObservableProperty]
        [JsonProperty("ipv4Gateway")]
        string ipv4Gateway;

        [ObservableProperty]
        [JsonProperty("ipv4MaskBits")]
        long ipv4MaskBits;

        [ObservableProperty]
        [JsonProperty("ipv4Mode")]
        string ipv4Mode;

        [ObservableProperty]
        [JsonProperty("ipv4Nameserver")]
        string ipv4Nameserver;

        [ObservableProperty]
        [JsonProperty("ipv6Address")]
        string ipv6Address;

        [ObservableProperty]
        [JsonProperty("ipv6Gateway")]
        string ipv6Gateway;

        [ObservableProperty]
        [JsonProperty("ipv6MaskBits")]
        long ipv6MaskBits;

        [ObservableProperty]
        [JsonProperty("ipv6Mode")]
        string ipv6Mode;

        [ObservableProperty]
        [JsonProperty("ipv6Nameserver")]
        string ipv6Nameserver;

        [ObservableProperty]
        [JsonProperty("manualManaged")]
        bool manualManaged;

        [ObservableProperty]
        [JsonProperty("password")]
        string password;

        [ObservableProperty]
        [JsonProperty("passwordMethod")]
        string passwordMethod;

        [ObservableProperty]
        [JsonProperty("ssid")]
        string ssid;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

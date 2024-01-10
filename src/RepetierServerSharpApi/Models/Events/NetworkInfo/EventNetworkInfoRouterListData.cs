using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoRouterListData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("active")]
        [property: JsonIgnore]
        bool active;

        [ObservableProperty]
        [JsonProperty("hidden")]
        [property: JsonIgnore]
        bool hidden;

        [ObservableProperty]
        [JsonProperty("ignore")]
        [property: JsonIgnore]
        bool ignore;

        [ObservableProperty]
        [JsonProperty("ipv4Address")]
        [property: JsonIgnore]
        string ipv4Address;

        [ObservableProperty]
        [JsonProperty("ipv4Gateway")]
        [property: JsonIgnore]
        string ipv4Gateway;

        [ObservableProperty]
        [JsonProperty("ipv4MaskBits")]
        [property: JsonIgnore]
        long ipv4MaskBits;

        [ObservableProperty]
        [JsonProperty("ipv4Mode")]
        [property: JsonIgnore]
        string ipv4Mode;

        [ObservableProperty]
        [JsonProperty("ipv4Nameserver")]
        [property: JsonIgnore]
        string ipv4Nameserver;

        [ObservableProperty]
        [JsonProperty("ipv6Address")]
        [property: JsonIgnore]
        string ipv6Address;

        [ObservableProperty]
        [JsonProperty("ipv6Gateway")]
        [property: JsonIgnore]
        string ipv6Gateway;

        [ObservableProperty]
        [JsonProperty("ipv6MaskBits")]
        [property: JsonIgnore]
        long ipv6MaskBits;

        [ObservableProperty]
        [JsonProperty("ipv6Mode")]
        [property: JsonIgnore]
        string ipv6Mode;

        [ObservableProperty]
        [JsonProperty("ipv6Nameserver")]
        [property: JsonIgnore]
        string ipv6Nameserver;

        [ObservableProperty]
        [JsonProperty("manualManaged")]
        [property: JsonIgnore]
        bool manualManaged;

        [ObservableProperty]
        [JsonProperty("password")]
        [property: JsonIgnore]
        string password;

        [ObservableProperty]
        [JsonProperty("passwordMethod")]
        [property: JsonIgnore]
        string passwordMethod;

        [ObservableProperty]
        [JsonProperty("ssid")]
        [property: JsonIgnore]
        string ssid;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

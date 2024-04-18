using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoRouterListData : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("active")]     
        bool active;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("hidden")]
        bool hidden;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ignore")]   
        bool ignore;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv4Address")]    
        string ipv4Address;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv4Gateway")]
        string ipv4Gateway;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv4MaskBits")]
        long ipv4MaskBits;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv4Mode")]
        string ipv4Mode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv4Nameserver")]
        string ipv4Nameserver;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv6Address")]
        string ipv6Address;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv6Gateway")]
        string ipv6Gateway;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv6MaskBits")]
        long ipv6MaskBits;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv6Mode")]
        
        string ipv6Mode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ipv6Nameserver")]
        
        string ipv6Nameserver;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("manualManaged")]
        
        bool manualManaged;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("password")]
        
        string password;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("passwordMethod")]
        
        string passwordMethod;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ssid")]
        
        string ssid;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

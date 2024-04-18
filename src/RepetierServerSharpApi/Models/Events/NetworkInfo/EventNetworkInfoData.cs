using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoData : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("activeRouter")]
        
        bool activeRouter;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("activeSSID")]
        
        string activeSsid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("apMode")]
        
        long apMode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("apSSID")]
        
        string apSsid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("channel")]
        
        long channel;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("channels")]
        
        List<long> channels = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("connections")]
        
        List<EventNetworkInfoConnection> connections = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("country")]
        
        string country;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("hostname")]
        
        string hostname;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("manageable")]
        
        bool manageable;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("mode")]
        
        long mode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("routerList")]
        
        List<EventNetworkInfoRouterList> routerList = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("screensaver")]
        
        bool screensaver;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timezone")]
        
        string timezone;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]
        
        long version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

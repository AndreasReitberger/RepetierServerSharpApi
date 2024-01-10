using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("activeRouter")]
        [property: JsonIgnore]
        bool activeRouter;

        [ObservableProperty]
        [JsonProperty("activeSSID")]
        [property: JsonIgnore]
        string activeSsid;

        [ObservableProperty]
        [JsonProperty("apMode")]
        [property: JsonIgnore]
        long apMode;

        [ObservableProperty]
        [JsonProperty("apSSID")]
        [property: JsonIgnore]
        string apSsid;

        [ObservableProperty]
        [JsonProperty("channel")]
        [property: JsonIgnore]
        long channel;

        [ObservableProperty]
        [JsonProperty("channels")]
        [property: JsonIgnore]
        List<long> channels = new();

        [ObservableProperty]
        [JsonProperty("connections")]
        [property: JsonIgnore]
        List<EventNetworkInfoConnection> connections = new();

        [ObservableProperty]
        [JsonProperty("country")]
        [property: JsonIgnore]
        string country;

        [ObservableProperty]
        [JsonProperty("hostname")]
        [property: JsonIgnore]
        string hostname;

        [ObservableProperty]
        [JsonProperty("manageable")]
        [property: JsonIgnore]
        bool manageable;

        [ObservableProperty]
        [JsonProperty("mode")]
        [property: JsonIgnore]
        long mode;

        [ObservableProperty]
        [JsonProperty("routerList")]
        [property: JsonIgnore]
        List<EventNetworkInfoRouterList> routerList = new();

        [ObservableProperty]
        [JsonProperty("screensaver")]
        [property: JsonIgnore]
        bool screensaver;

        [ObservableProperty]
        [JsonProperty("timezone")]
        [property: JsonIgnore]
        string timezone;

        [ObservableProperty]
        [JsonProperty("version")]
        [property: JsonIgnore]
        long version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

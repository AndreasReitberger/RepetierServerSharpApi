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
        bool activeRouter;

        [ObservableProperty]
        [JsonProperty("activeSSID")]
        string activeSsid;

        [ObservableProperty]
        [JsonProperty("apMode")]
        long apMode;

        [ObservableProperty]
        [JsonProperty("apSSID")]
        string apSsid;

        [ObservableProperty]
        [JsonProperty("channel")]
        long channel;

        [ObservableProperty]
        [JsonProperty("channels")]
        List<long> channels = new();

        [ObservableProperty]
        [JsonProperty("connections")]
        List<EventNetworkInfoConnection> connections = new();

        [ObservableProperty]
        [JsonProperty("country")]
        string country;

        [ObservableProperty]
        [JsonProperty("hostname")]
        string hostname;

        [ObservableProperty]
        [JsonProperty("manageable")]
        bool manageable;

        [ObservableProperty]
        [JsonProperty("mode")]
        long mode;

        [ObservableProperty]
        [JsonProperty("routerList")]
        List<EventNetworkInfoRouterList> routerList = new();

        [ObservableProperty]
        [JsonProperty("screensaver")]
        bool screensaver;

        [ObservableProperty]
        [JsonProperty("timezone")]
        string timezone;

        [ObservableProperty]
        [JsonProperty("version")]
        long version;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

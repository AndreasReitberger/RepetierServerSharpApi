using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoRouterList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("SSID")]
        public string ssid;

        [ObservableProperty]
        [JsonProperty("active")]
        public bool active;

        [ObservableProperty]
        [JsonProperty("bars")]
        public long bars;

        [ObservableProperty]
        [JsonProperty("channel")]
        public long channel;

        [ObservableProperty]
        [JsonProperty("data")]
        public EventNetworkInfoRouterListData data;

        [ObservableProperty]
        [JsonProperty("mode")]
        public string mode;

        [ObservableProperty]
        [JsonProperty("rate")]
        public string rate;

        [ObservableProperty]
        [JsonProperty("secure")]
        public bool secure;

        [ObservableProperty]
        [JsonProperty("signal")]
        public long signal;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

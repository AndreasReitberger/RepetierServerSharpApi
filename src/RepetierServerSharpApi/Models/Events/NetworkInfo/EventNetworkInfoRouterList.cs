using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoRouterList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("SSID")]
        [property: JsonIgnore]
        public string ssid;

        [ObservableProperty]
        [JsonProperty("active")]
        [property: JsonIgnore]
        public bool active;

        [ObservableProperty]
        [JsonProperty("bars")]
        [property: JsonIgnore]
        public long bars;

        [ObservableProperty]
        [JsonProperty("channel")]
        [property: JsonIgnore]
        public long channel;

        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        public EventNetworkInfoRouterListData data;

        [ObservableProperty]
        [JsonProperty("mode")]
        [property: JsonIgnore]
        public string mode;

        [ObservableProperty]
        [JsonProperty("rate")]
        [property: JsonIgnore]
        public string rate;

        [ObservableProperty]
        [JsonProperty("secure")]
        [property: JsonIgnore]
        public bool secure;

        [ObservableProperty]
        [JsonProperty("signal")]
        [property: JsonIgnore]
        public long signal;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

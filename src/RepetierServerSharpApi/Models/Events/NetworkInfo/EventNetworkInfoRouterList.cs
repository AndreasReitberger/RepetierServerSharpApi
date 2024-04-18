using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoRouterList : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("SSID")]

        public string ssid = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("active")]

        public bool active;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("bars")]

        public long bars;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("channel")]

        public long channel;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("data")]

        public EventNetworkInfoRouterListData? data;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("mode")]

        public string mode = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("rate")]

        public string rate = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("secure")]

        public bool secure;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("signal")]

        public long signal;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

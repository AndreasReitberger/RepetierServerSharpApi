using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RouterList : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("SSID")]

        string ssid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("active")]

        bool? active;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("bars")]

        long? bars;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("channel")]

        long? channel;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("data")]

        ConnectionData data;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("mode")]

        string mode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("rate")]

        string rate;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("secure")]

        bool? secure;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("signal")]

        long? signal;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

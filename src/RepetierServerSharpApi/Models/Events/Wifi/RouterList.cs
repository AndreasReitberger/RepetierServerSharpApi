using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RouterList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("SSID")]
        [property: JsonIgnore]
        string ssid;

        [ObservableProperty]
        [JsonProperty("active")]
        [property: JsonIgnore]
        bool? active;

        [ObservableProperty]
        [JsonProperty("bars")]
        [property: JsonIgnore]
        long? bars;

        [ObservableProperty]
        [JsonProperty("channel")]
        [property: JsonIgnore]
        long? channel;

        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        ConnectionData data;

        [ObservableProperty]
        [JsonProperty("mode")]
        [property: JsonIgnore]
        string mode;

        [ObservableProperty]
        [JsonProperty("rate")]
        [property: JsonIgnore]
        string rate;

        [ObservableProperty]
        [JsonProperty("secure")]
        [property: JsonIgnore]
        bool? secure;

        [ObservableProperty]
        [JsonProperty("signal")]
        [property: JsonIgnore]
        long? signal;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RouterList : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("SSID")]
        public partial string Ssid { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("active")]
        public partial bool? Active { get; set; }

        [ObservableProperty]

        [JsonProperty("bars")]
        public partial long? Bars { get; set; }

        [ObservableProperty]

        [JsonProperty("channel")]
        public partial long? Channel { get; set; }

        [ObservableProperty]

        [JsonProperty("data")]
        public partial ConnectionData? Data { get; set; }

        [ObservableProperty]

        [JsonProperty("mode")]
        public partial string Mode { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("rate")]
        public partial string Rate { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("secure")]
        public partial bool? Secure { get; set; }

        [ObservableProperty]

        [JsonProperty("signal")]
        public partial long? Signal { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RouterList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("SSID"), JsonPropertyName("SSID")]
        public partial string Ssid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("active"), JsonPropertyName("active")]
        public partial bool? Active { get; set; }

        [ObservableProperty]
        [JsonProperty("bars"), JsonPropertyName("bars")]
        public partial long? Bars { get; set; }

        [ObservableProperty]
        [JsonProperty("channel"), JsonPropertyName("channel")]
        public partial long? Channel { get; set; }

        [ObservableProperty]
        [JsonProperty("data"), JsonPropertyName("data")]
        public partial ConnectionData? Data { get; set; }

        [ObservableProperty]
        [JsonProperty("mode"), JsonPropertyName("mode")]
        public partial string Mode { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("rate"), JsonPropertyName("rate")]
        public partial string Rate { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("secure"), JsonPropertyName("secure")]
        public partial bool? Secure { get; set; }

        [ObservableProperty]
        [JsonProperty("signal"), JsonPropertyName("signal")]
        public partial long? Signal { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("activeRouter"), JsonPropertyName("activeRouter")]
        public partial bool ActiveRouter { get; set; }

        [ObservableProperty]
        [JsonProperty("activeSSID"), JsonPropertyName("activeSSID")]
        public partial string ActiveSsid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("apMode"), JsonPropertyName("apMode")]
        public partial long ApMode { get; set; }

        [ObservableProperty]
        [JsonProperty("apSSID"), JsonPropertyName("apSSID")]
        public partial string ApSsid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("channel"), JsonPropertyName("channel")]
        public partial long Channel { get; set; }

        [ObservableProperty]
        [JsonProperty("channels"), JsonPropertyName("channels")]
        public partial List<long> Channels { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("connections"), JsonPropertyName("connections")]
        public partial List<EventNetworkInfoConnection> Connections { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("country"), JsonPropertyName("country")]
        public partial string Country { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("hostname"), JsonPropertyName("hostname")]
        public partial string Hostname { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("manageable"), JsonPropertyName("manageable")]
        public partial bool Manageable { get; set; }

        [ObservableProperty]
        [JsonProperty("mode"), JsonPropertyName("mode")]
        public partial long Mode { get; set; }

        [ObservableProperty]
        [JsonProperty("routerList"), JsonPropertyName("routerList")]
        public partial List<EventNetworkInfoRouterList> RouterList { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("screensaver"), JsonPropertyName("screensaver")]
        public partial bool Screensaver { get; set; }

        [ObservableProperty]
        [JsonProperty("timezone"), JsonPropertyName("timezone")]
        public partial string Timezone { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("version"), JsonPropertyName("version")]
        public partial long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

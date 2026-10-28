namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("activeRouter")]
        public partial bool ActiveRouter { get; set; }

        [ObservableProperty]
        [JsonPropertyName("activeSSID")]
        public partial string ActiveSsid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("apMode")]
        public partial long ApMode { get; set; }

        [ObservableProperty]
        [JsonPropertyName("apSSID")]
        public partial string ApSsid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("channel")]
        public partial long Channel { get; set; }

        [ObservableProperty]
        [JsonPropertyName("channels")]
        public partial List<long> Channels { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("connections")]
        public partial List<EventNetworkInfoConnection> Connections { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("country")]
        public partial string Country { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("hostname")]
        public partial string Hostname { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("manageable")]
        public partial bool Manageable { get; set; }

        [ObservableProperty]
        [JsonPropertyName("mode")]
        public partial long Mode { get; set; }

        [ObservableProperty]
        [JsonPropertyName("routerList")]
        public partial List<EventNetworkInfoRouterList> RouterList { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("screensaver")]
        public partial bool Screensaver { get; set; }

        [ObservableProperty]
        [JsonPropertyName("timezone")]
        public partial string Timezone { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("version")]
        public partial long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventNetworkInfoData);
        #endregion
    }
}

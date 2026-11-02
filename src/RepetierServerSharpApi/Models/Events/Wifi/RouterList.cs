namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RouterList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("SSID")]
        public partial string Ssid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("active")]
        public partial bool? Active { get; set; }

        [ObservableProperty]
        [JsonPropertyName("bars")]
        public partial long? Bars { get; set; }

        [ObservableProperty]
        [JsonPropertyName("channel")]
        public partial long? Channel { get; set; }

        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial ConnectionData? Data { get; set; }

        [ObservableProperty]
        [JsonPropertyName("mode")]
        public partial string Mode { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("rate")]
        public partial string Rate { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("secure")]
        public partial bool? Secure { get; set; }

        [ObservableProperty]
        [JsonPropertyName("signal")]
        public partial long? Signal { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RouterList);
        #endregion
    }
}

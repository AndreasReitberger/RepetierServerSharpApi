namespace AndreasReitberger.API.Repetier.Models
{
    public partial class HardwareInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("icon")]
        public partial long? Icon { get; set; }

        [ObservableProperty]
        [JsonPropertyName("msgType")]
        public partial long? MsgType { get; set; }

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("text")]
        public partial string Text { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("unit")]
        public partial string Unit { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("urgency")]
        public partial long? Urgency { get; set; }

        [ObservableProperty]
        [JsonPropertyName("url")]
        public partial string Url { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("value")]
        public partial double? Value { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.HardwareInfo);
        #endregion
    }
}

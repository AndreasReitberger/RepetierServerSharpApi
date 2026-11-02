namespace AndreasReitberger.API.Repetier.Models
{
    public partial class WifiConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("SSID")]
        public partial string Ssid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("device")]
        public partial string Device { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.WifiConnection);
        #endregion
    }
}

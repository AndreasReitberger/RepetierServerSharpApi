namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierQuickGcodeCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("command")]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("visibleWhenPrinting")]
        public partial bool VisibleWhenPrinting { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierQuickGcodeCommand);

        #endregion
    }
}

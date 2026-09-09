namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierLicenseInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        [JsonPropertyName("hasBranding")]
        public partial bool HasBranding { get; set; }

        [ObservableProperty]
        [JsonPropertyName("licence")]
        public partial string Licence { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("wantsBranding")]
        public partial bool WantsBranding { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierLicenseInfo);
        #endregion
    }

}

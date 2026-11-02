namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("login")]
        public partial string Login { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("permissions")]
        public partial long Permissions { get; set; }

        [ObservableProperty]
        [JsonPropertyName("settings")]
        public partial EventUserCredentialsSettings? Settings { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventUserCredentialsData);
        #endregion
    }
}

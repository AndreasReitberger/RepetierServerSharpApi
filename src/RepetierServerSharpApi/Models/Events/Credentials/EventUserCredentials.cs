namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentials : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial EventUserCredentialsData? Data { get; set; }

        [ObservableProperty]
        [JsonPropertyName("event")]
        public partial string EventName { get; set; } = string.Empty;

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventUserCredentials);
        #endregion
    }
}

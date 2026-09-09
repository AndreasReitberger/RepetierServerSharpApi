namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventSession : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("callback_id")]
        public partial long CallbackId { get; set; }

        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial object? Data { get; set; }

        [ObservableProperty]
        [JsonPropertyName("session")]
        public partial string Session { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventSession);
        #endregion
    }
}

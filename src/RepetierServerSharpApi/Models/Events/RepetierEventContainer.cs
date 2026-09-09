namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierEventContainer : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("callback_id")]
        public partial long CallbackId { get; set; }

        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial List<RepetierEventData> Data { get; set; } = new();

        [ObservableProperty]
        [JsonPropertyName("eventList")]
        public partial bool EventList { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierEventContainer);
        #endregion
    }
}

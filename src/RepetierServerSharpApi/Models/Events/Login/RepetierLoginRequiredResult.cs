namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginRequiredResult : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("callback_id")]
        public partial long? CallbackId { get; set; }

        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial List<RepetierLoginRequiredResultData> Data { get; set; } = new();

        [ObservableProperty]
        [JsonPropertyName("eventList")]
        public partial bool? EventList { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierLoginRequiredResult);

        #endregion
    }
}

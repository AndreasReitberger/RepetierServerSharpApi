namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventGcodeInfoUpdatedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("list")]
        public partial string List { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("modelId")]
        public partial long ModelId { get; set; }

        [ObservableProperty]
        [JsonPropertyName("modelPath")]
        public partial string ModelPath { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventGcodeInfoUpdatedData);
        #endregion
    }
}

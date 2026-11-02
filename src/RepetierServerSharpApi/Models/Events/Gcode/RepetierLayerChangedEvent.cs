namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLayerChangedEvent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("layer")]
        public partial long Layer { get; set; }

        [ObservableProperty]
        [JsonPropertyName("maxLayer")]
        public partial long MaxLayer { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierLayerChangedEvent);
        #endregion
    }
}

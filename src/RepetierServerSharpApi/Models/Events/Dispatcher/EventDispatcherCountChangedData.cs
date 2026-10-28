namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventDispatcherCountChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("count")]
        public partial long Count { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventDispatcherCountChangedData);
        #endregion
    }
}

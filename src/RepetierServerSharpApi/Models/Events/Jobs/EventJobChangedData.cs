namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobChangedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventJobChangedData);

        #endregion
    }

}

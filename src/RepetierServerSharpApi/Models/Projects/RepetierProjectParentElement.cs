namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectParentElement : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("empty")]
        public partial bool Empty { get; set; }

        [ObservableProperty]
        [JsonPropertyName("idx")]
        public partial long Idx { get; set; }

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierProjectParentElement);
        #endregion
    }
}

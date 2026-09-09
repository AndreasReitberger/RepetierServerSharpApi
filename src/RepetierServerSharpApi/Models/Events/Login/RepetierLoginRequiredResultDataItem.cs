namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginRequiredResultDataItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("session")]
        public partial string Session { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierLoginRequiredResultDataItem);
        #endregion
    }
}

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierActionResult : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("ok")]
        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this, RepetierSourceGenerationContext.Default.RepetierActionResult);

        #endregion
    }
}

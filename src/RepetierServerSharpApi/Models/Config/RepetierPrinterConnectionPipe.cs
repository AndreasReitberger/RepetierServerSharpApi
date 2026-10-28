namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionPipe : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("file")]
        public partial string File { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConnectionPipe);
        #endregion
    }
}

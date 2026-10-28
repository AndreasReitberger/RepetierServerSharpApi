namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigProperties : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("firmwareuploader_board")]
        public partial string FirmwareuploaderBoard { get; set; }

        [ObservableProperty]
        [JsonPropertyName("firmwareuploader_extraPort")]
        public partial string FirmwareuploaderExtraPort { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigProperties);
        #endregion
    }
}

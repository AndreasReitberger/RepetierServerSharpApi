namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterListRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial List<RepetierPrinter> Printers { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterListRespone);
        #endregion
    }
}

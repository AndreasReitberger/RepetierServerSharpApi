namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigTemperature : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("temp")]
        public partial double Temp { get; set; }

        #region Json Ignore
        [ObservableProperty]
        public partial string TargetComponent { get; set; } = string.Empty;
        #endregion

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigTemperature);

        #endregion
    }
}

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGpioListRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("list")]
        public partial List<RepetierGpioListItem> List { get; set; } = new();

        [ObservableProperty]
        [JsonPropertyName("ok")]
        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierGpioListRespone);
        #endregion
    }
}

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistorySummaryRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("list")]
        public partial List<RepetierHistorySummaryItem> Summaries { get; set; } = new();

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierHistorySummaryRespone);
        #endregion
    }

}

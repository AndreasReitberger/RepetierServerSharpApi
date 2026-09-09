namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistoryListRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("list")]
        public partial List<RepetierHistoryListItem> List { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierHistoryListRespone);
        #endregion
    }
}

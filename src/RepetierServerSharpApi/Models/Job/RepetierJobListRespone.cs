namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierJobListRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial List<RepetierJobListItem> Data { get; set; } = [];

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierJobListRespone);
        #endregion
    }
}

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial List<RepetierModel> Data { get; set; } = [];

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierModelList);
        #endregion
    }
}

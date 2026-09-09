namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCallList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("list")]
        public partial List<RepetierWebCallAction> List { get; set; } = new();

        [ObservableProperty]
        [JsonPropertyName("ok")]
        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierWebCallList);
        #endregion
    }
}

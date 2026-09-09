namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelGroups : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("groupNames")]
        public partial string[] GroupNames { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("ok")]
        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierModelGroups);
        #endregion
    }
}

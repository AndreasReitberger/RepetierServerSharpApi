namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("ok")]
        public partial bool? Ok { get; set; }

        [ObservableProperty]
        [JsonPropertyName("project")]
        public partial RepetierProjectsProject? Project { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierProjectsProjectRespone);
        #endregion
    }
}

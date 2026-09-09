namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsServerListRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonPropertyName("ok")]
        public partial bool Ok { get; set; }

        [ObservableProperty]

        [JsonPropertyName("server")]
        public partial List<ProjectsServer> Server { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierProjectsServerListRespone);
        #endregion
    }

}

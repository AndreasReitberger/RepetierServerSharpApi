namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectFolder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("empty")]
        public partial bool Empty { get; set; }

        [ObservableProperty]
        [JsonPropertyName("folders")]
        public partial List<RepetierProjectSubFolder> Folders { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("idx")]
        public partial long Idx { get; set; }

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("parents")]
        public partial List<RepetierProjectParentElement> Parents { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("projects")]
        public partial List<RepetierProject> Projects { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("version")]
        public partial long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierProjectFolder);
        #endregion
    }
}

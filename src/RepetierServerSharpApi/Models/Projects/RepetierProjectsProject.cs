using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProject : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("author")]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("comments")]
        public partial List<RepetierProjectsProjectComment> Comments { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("created")]
        public partial long? Created { get; set; }

        [ObservableProperty]
        [JsonPropertyName("description")]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("descriptionHtml")]
        public partial string DescriptionHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("images")]
        public partial List<RepetierProjectsProjectFile> Images { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("instructions")]
        public partial string Instructions { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("instructionsHtml")]
        public partial string InstructionsHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("license")]
        public partial string License { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("licenseFile")]
        public partial string LicenseFile { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("licenseHtml")]
        public partial string LicenseHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("models")]
        public partial List<RepetierProjectsProjectFile> Models { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("openNewFiles")]
        public partial long? OpenNewFiles { get; set; }

        [ObservableProperty]
        [JsonPropertyName("others")]
        public partial List<RepetierProjectsProjectFile> Others { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("parents")]
        public partial List<RepetierProjectsProjectParent> Parents { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("preview")]
        public partial string Preview { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("tags")]
        public partial List<string> Tags { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("uuid")]
        public partial Guid? Uuid { get; set; }

        [ObservableProperty]
        [JsonPropertyName("version")]
        public partial long? Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierProjectsProject);
        #endregion
    }
}

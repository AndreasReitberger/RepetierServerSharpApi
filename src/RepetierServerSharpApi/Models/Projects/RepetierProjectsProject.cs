using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProject : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("author"), JsonPropertyName("author")]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("comments"), JsonPropertyName("comments")]
        public partial List<RepetierProjectsProjectComment> Comments { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("created"), JsonPropertyName("created")]
        public partial long? Created { get; set; }

        [ObservableProperty]
        [JsonProperty("description"), JsonPropertyName("description")]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("descriptionHtml"), JsonPropertyName("descriptionHtml")]
        public partial string DescriptionHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("images"), JsonPropertyName("images")]
        public partial List<RepetierProjectsProjectFile> Images { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("instructions"), JsonPropertyName("instructions")]
        public partial string Instructions { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("instructionsHtml"), JsonPropertyName("instructionsHtml")]
        public partial string InstructionsHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("license"), JsonPropertyName("license")]
        public partial string License { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("licenseFile"), JsonPropertyName("licenseFile")]
        public partial string LicenseFile { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("licenseHtml"), JsonPropertyName("licenseHtml")]
        public partial string LicenseHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("models"), JsonPropertyName("models")]
        public partial List<RepetierProjectsProjectFile> Models { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("openNewFiles"), JsonPropertyName("openNewFiles")]
        public partial long? OpenNewFiles { get; set; }

        [ObservableProperty]
        [JsonProperty("others"), JsonPropertyName("others")]
        public partial List<RepetierProjectsProjectFile> Others { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("parents"), JsonPropertyName("parents")]
        public partial List<RepetierProjectsProjectParent> Parents { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("preview"), JsonPropertyName("preview")]
        public partial string Preview { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("tags"), JsonPropertyName("tags")]
        public partial List<string> Tags { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("uuid"), JsonPropertyName("uuid")]
        public partial Guid? Uuid { get; set; }

        [ObservableProperty]
        [JsonProperty("version"), JsonPropertyName("version")]
        public partial long? Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

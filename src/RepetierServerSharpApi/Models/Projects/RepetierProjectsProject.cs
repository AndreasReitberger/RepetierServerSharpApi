using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProject : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("author")]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("comments")]
        public partial List<RepetierProjectsProjectComment> Comments { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("created")]
        public partial long? Created { get; set; }

        [ObservableProperty]
        
        [JsonProperty("description")]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("descriptionHtml")]
        public partial string DescriptionHtml { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("images")]
        public partial List<RepetierProjectsProjectFile> Images { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("instructions")]
        public partial string Instructions { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("instructionsHtml")]
        public partial string InstructionsHtml { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("license")]
        public partial string License { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("licenseFile")]
        public partial string LicenseFile { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("licenseHtml")]
        public partial string LicenseHtml { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("models")]
        public partial List<RepetierProjectsProjectFile> Models { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("openNewFiles")]
        public partial long? OpenNewFiles { get; set; }

        [ObservableProperty]
        
        [JsonProperty("others")]
        public partial List<RepetierProjectsProjectFile> Others { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("parents")]
        public partial List<RepetierProjectsProjectParent> Parents { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("preview")]
        public partial string Preview { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("tags")]
        public partial List<string> Tags { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("uuid")]
        public partial Guid? Uuid { get; set; }

        [ObservableProperty]
        
        [JsonProperty("version")]
        public partial long? Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProject
    {
        #region Properties
        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        [JsonProperty("comments", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierProjectsProjectComment> Comments { get; set; } = new();

        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public long? Created { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("descriptionHtml", NullValueHandling = NullValueHandling.Ignore)]
        public string DescriptionHtml { get; set; }

        [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierProjectsProjectFile> Images { get; set; } = new();

        [JsonProperty("instructions", NullValueHandling = NullValueHandling.Ignore)]
        public string Instructions { get; set; }

        [JsonProperty("instructionsHtml", NullValueHandling = NullValueHandling.Ignore)]
        public string InstructionsHtml { get; set; }

        [JsonProperty("license", NullValueHandling = NullValueHandling.Ignore)]
        public string License { get; set; }

        [JsonProperty("licenseFile", NullValueHandling = NullValueHandling.Ignore)]
        public string LicenseFile { get; set; }

        [JsonProperty("licenseHtml", NullValueHandling = NullValueHandling.Ignore)]
        public string LicenseHtml { get; set; }

        [JsonProperty("models", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierProjectsProjectFile> Models { get; set; } = new();

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("openNewFiles", NullValueHandling = NullValueHandling.Ignore)]
        public long? OpenNewFiles { get; set; }

        [JsonProperty("others", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierProjectsProjectFile> Others { get; set; } = new();

        [JsonProperty("parents", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierProjectsProjectParent> Parents { get; set; } = new();

        [JsonProperty("preview", NullValueHandling = NullValueHandling.Ignore)]
        public string Preview { get; set; }

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tags { get; set; } = new();

        [JsonProperty("uuid", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Uuid { get; set; }

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public long? Version { get; set; }
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

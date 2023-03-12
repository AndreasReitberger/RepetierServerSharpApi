using CommunityToolkit.Mvvm.ComponentModel;
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
        public string author;

        [ObservableProperty]
        [JsonProperty("comments")]
        public List<RepetierProjectsProjectComment> comments = new();

        [ObservableProperty]
        [JsonProperty("created")]
        public long? created;

        [ObservableProperty]
        [JsonProperty("description")]
        public string description;

        [ObservableProperty]
        [JsonProperty("descriptionHtml")]
        public string descriptionHtml;

        [ObservableProperty]
        [JsonProperty("images")]
        public List<RepetierProjectsProjectFile> images = new();

        [ObservableProperty]
        [JsonProperty("instructions")]
        public string instructions;

        [ObservableProperty]
        [JsonProperty("instructionsHtml")]
        public string instructionsHtml;

        [ObservableProperty]
        [JsonProperty("license")]
        public string license;

        [ObservableProperty]
        [JsonProperty("licenseFile")]
        public string licenseFile;

        [ObservableProperty]
        [JsonProperty("licenseHtml")]
        public string licenseHtml;

        [ObservableProperty]
        [JsonProperty("models")]
        public List<RepetierProjectsProjectFile> models = new();

        [ObservableProperty]
        [JsonProperty("name")]
        public string name;

        [ObservableProperty]
        [JsonProperty("openNewFiles")]
        public long? openNewFiles;

        [ObservableProperty]
        [JsonProperty("others")]
        public List<RepetierProjectsProjectFile> others = new();

        [ObservableProperty]
        [JsonProperty("parents")]
        public List<RepetierProjectsProjectParent> parents = new();

        [ObservableProperty]
        [JsonProperty("preview")]
        public string preview;

        [ObservableProperty]
        [JsonProperty("tags")]
        public List<string> tags = new();

        [ObservableProperty]
        [JsonProperty("uuid")]
        public Guid? uuid;

        [ObservableProperty]
        [JsonProperty("version")]
        public long? version;
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

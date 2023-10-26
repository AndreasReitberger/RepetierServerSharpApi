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
        [property: JsonIgnore]
        public string author;

        [ObservableProperty]
        [JsonProperty("comments")]
        [property: JsonIgnore]
        public List<RepetierProjectsProjectComment> comments = new();

        [ObservableProperty]
        [JsonProperty("created")]
        [property: JsonIgnore]
        public long? created;

        [ObservableProperty]
        [JsonProperty("description")]
        [property: JsonIgnore]
        public string description;

        [ObservableProperty]
        [JsonProperty("descriptionHtml")]
        [property: JsonIgnore]
        public string descriptionHtml;

        [ObservableProperty]
        [JsonProperty("images")]
        [property: JsonIgnore]
        public List<RepetierProjectsProjectFile> images = new();

        [ObservableProperty]
        [JsonProperty("instructions")]
        [property: JsonIgnore]
        public string instructions;

        [ObservableProperty]
        [JsonProperty("instructionsHtml")]
        [property: JsonIgnore]
        public string instructionsHtml;

        [ObservableProperty]
        [JsonProperty("license")]
        [property: JsonIgnore]
        public string license;

        [ObservableProperty]
        [JsonProperty("licenseFile")]
        [property: JsonIgnore]
        public string licenseFile;

        [ObservableProperty]
        [JsonProperty("licenseHtml")]
        [property: JsonIgnore]
        public string licenseHtml;

        [ObservableProperty]
        [JsonProperty("models")]
        [property: JsonIgnore]
        public List<RepetierProjectsProjectFile> models = new();

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        public string name;

        [ObservableProperty]
        [JsonProperty("openNewFiles")]
        [property: JsonIgnore]
        public long? openNewFiles;

        [ObservableProperty]
        [JsonProperty("others")]
        [property: JsonIgnore]
        public List<RepetierProjectsProjectFile> others = new();

        [ObservableProperty]
        [JsonProperty("parents")]
        [property: JsonIgnore]
        public List<RepetierProjectsProjectParent> parents = new();

        [ObservableProperty]
        [JsonProperty("preview")]
        [property: JsonIgnore]
        public string preview;

        [ObservableProperty]
        [JsonProperty("tags")]
        [property: JsonIgnore]
        public List<string> tags = new();

        [ObservableProperty]
        [JsonProperty("uuid")]
        [property: JsonIgnore]
        public Guid? uuid;

        [ObservableProperty]
        [JsonProperty("version")]
        [property: JsonIgnore]
        public long? version;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

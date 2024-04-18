using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProject : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("author")]

        public string author;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("comments")]

        public List<RepetierProjectsProjectComment> comments = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("created")]

        public long? created;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("description")]

        public string description;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("descriptionHtml")]

        public string descriptionHtml;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("images")]

        public List<RepetierProjectsProjectFile> images = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("instructions")]

        public string instructions;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("instructionsHtml")]

        public string instructionsHtml;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("license")]

        public string license;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("licenseFile")]

        public string licenseFile;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("licenseHtml")]

        public string licenseHtml;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("models")]

        public List<RepetierProjectsProjectFile> models = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]

        public string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("openNewFiles")]

        public long? openNewFiles;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("others")]

        public List<RepetierProjectsProjectFile> others = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("parents")]

        public List<RepetierProjectsProjectParent> parents = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("preview")]

        public string preview;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tags")]

        public List<string> tags = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("uuid")]

        public Guid? uuid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]

        public long? version;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

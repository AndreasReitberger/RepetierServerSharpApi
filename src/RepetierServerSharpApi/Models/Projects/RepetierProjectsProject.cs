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
        string author = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("comments")]
        List<RepetierProjectsProjectComment> comments = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("created")]

        long? created;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("description")]

        string description = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("descriptionHtml")]

        string descriptionHtml = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("images")]

        List<RepetierProjectsProjectFile> images = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("instructions")]

        string instructions = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("instructionsHtml")]

        string instructionsHtml = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("license")]

        string license = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("licenseFile")]

        string licenseFile = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("licenseHtml")]

        string licenseHtml = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("models")]

        List<RepetierProjectsProjectFile> models = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("openNewFiles")]
        long? openNewFiles;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("others")]
        List<RepetierProjectsProjectFile> others = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("parents")]
        List<RepetierProjectsProjectParent> parents = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("preview")]
        string preview = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tags")]
        List<string> tags = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("uuid")]
        Guid? uuid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]
        long? version;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

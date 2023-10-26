using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectFolder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("empty")]
        [property: JsonIgnore]
        bool empty;

        [ObservableProperty]
        [JsonProperty("folders")]
        [property: JsonIgnore]
        List<RepetierProjectSubFolder> folders = new();

        [ObservableProperty]
        [JsonProperty("idx")]
        [property: JsonIgnore]
        long idx;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("parents")]
        [property: JsonIgnore]
        List<RepetierProjectParentElement> parents = new();

        [ObservableProperty]
        [JsonProperty("projects")]
        [property: JsonIgnore]
        List<RepetierProject> projects = new();

        [ObservableProperty]
        [JsonProperty("version")]
        [property: JsonIgnore]
        long version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

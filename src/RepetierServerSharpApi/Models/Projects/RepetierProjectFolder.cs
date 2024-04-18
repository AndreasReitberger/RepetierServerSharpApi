using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectFolder : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("empty")]

        bool empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("folders")]

        List<RepetierProjectSubFolder> folders = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("idx")]

        long idx;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]

        string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("parents")]

        List<RepetierProjectParentElement> parents = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("projects")]

        List<RepetierProject> projects = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]

        long version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

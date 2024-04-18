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
        List<RepetierProjectSubFolder> folders = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("idx")]
        long idx;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("parents")]
        List<RepetierProjectParentElement> parents = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("projects")]
        List<RepetierProject> projects = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]
        long version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

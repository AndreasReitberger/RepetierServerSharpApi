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
        bool empty;

        [ObservableProperty]
        [JsonProperty("folders")]
        List<RepetierProjectSubFolder> folders = new();

        [ObservableProperty]
        [JsonProperty("idx")]
        long idx;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("parents")]
        List<RepetierProjectParentElement> parents = new();

        [ObservableProperty]
        [JsonProperty("projects")]
        List<RepetierProject> projects = new();

        [ObservableProperty]
        [JsonProperty("version")]
        long version;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

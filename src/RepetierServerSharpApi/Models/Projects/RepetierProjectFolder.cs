using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectFolder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("empty")]
        public partial bool Empty { get; set; }

        [ObservableProperty]
        
        [JsonProperty("folders")]
        public partial List<RepetierProjectSubFolder> Folders { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("idx")]
        public partial long Idx { get; set; }

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("parents")]
        public partial List<RepetierProjectParentElement> Parents { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("projects")]
        public partial List<RepetierProject> Projects { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("version")]
        public partial long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

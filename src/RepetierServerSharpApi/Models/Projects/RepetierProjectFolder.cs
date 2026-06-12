using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectFolder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("empty"), JsonPropertyName("empty")]
        public partial bool Empty { get; set; }

        [ObservableProperty]
        [JsonProperty("folders"), JsonPropertyName("folders")]
        public partial List<RepetierProjectSubFolder> Folders { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("idx"), JsonPropertyName("idx")]
        public partial long Idx { get; set; }

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("parents"), JsonPropertyName("parents")]
        public partial List<RepetierProjectParentElement> Parents { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("projects"), JsonPropertyName("projects")]
        public partial List<RepetierProject> Projects { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("version"), JsonPropertyName("version")]
        public partial long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

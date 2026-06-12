using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectSubFolder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("empty"), JsonPropertyName("empty")]
        public partial bool Empty { get; set; }

        [ObservableProperty]
        [JsonProperty("idx"), JsonPropertyName("idx")]
        public partial long Idx { get; set; }

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectFile : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("n"), JsonPropertyName("n")]
        public partial string N { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("s"), JsonPropertyName("s")]
        public partial long? S { get; set; }

        [ObservableProperty]
        [JsonProperty("p"), JsonPropertyName("p")]
        public partial string P { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

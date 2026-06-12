using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectComment : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("comment"), JsonPropertyName("comment")]
        public partial string Comment { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("time"), JsonPropertyName("time")]
        public partial long? Time { get; set; }

        [ObservableProperty]
        [JsonProperty("user"), JsonPropertyName("user")]
        public partial string User { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

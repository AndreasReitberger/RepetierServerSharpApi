using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("gcodeGroup"), JsonPropertyName("gcodeGroup")]
        public partial string GcodeGroup { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("gcodeSortBy"), JsonPropertyName("gcodeSortBy")]
        public partial long GcodeSortBy { get; set; }

        [ObservableProperty]
        [JsonProperty("gcodeViewMode"), JsonPropertyName("gcodeViewMode")]
        public partial long GcodeViewMode { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

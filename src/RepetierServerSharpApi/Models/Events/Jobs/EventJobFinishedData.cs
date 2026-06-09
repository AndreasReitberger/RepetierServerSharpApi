using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobFinishedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("duration"), JsonPropertyName("duration")]
        public partial long? Duration { get; set; }

        [ObservableProperty]
        [JsonProperty("end"), JsonPropertyName("end")]
        public partial long? End { get; set; }

        [ObservableProperty]
        [JsonProperty("lines"), JsonPropertyName("lines")]
        public partial long? Lines { get; set; }

        [ObservableProperty]
        [JsonProperty("start"), JsonPropertyName("start")]
        public partial long? Start { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

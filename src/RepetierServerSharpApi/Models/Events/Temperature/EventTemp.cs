using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTemp : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("data"), JsonPropertyName("data")]
        public partial EventTempData? Data { get; set; }

        [ObservableProperty]
        [JsonProperty("event"), JsonPropertyName("event")]
        public partial string EventName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("printer"), JsonPropertyName("printer")]
        public partial string Printer { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

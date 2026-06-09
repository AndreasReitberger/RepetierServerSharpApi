using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("active"), JsonPropertyName("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        [JsonProperty("job"), JsonPropertyName("job")]
        public partial string Job { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("online"), JsonPropertyName("online")]
        public partial long Online { get; set; }

        [ObservableProperty]
        [JsonProperty("pauseState"), JsonPropertyName("pauseState")]
        public partial long PauseState { get; set; }

        [ObservableProperty]
        [JsonProperty("paused"), JsonPropertyName("paused")]
        public partial bool Paused { get; set; }

        [ObservableProperty]
        [JsonProperty("slug"), JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

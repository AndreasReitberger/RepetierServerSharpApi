using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventMessageChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("date"), JsonPropertyName("date")]
        public partial DateTimeOffset? Date { get; set; }

        [ObservableProperty]
        [JsonProperty("id"), JsonPropertyName("id")]
        public partial long? Id { get; set; }

        [ObservableProperty]
        [JsonProperty("link"), JsonPropertyName("link")]
        public partial string Link { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("msg"), JsonPropertyName("msg")]
        public partial string Msg { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("pause"), JsonPropertyName("pause")]
        public partial bool? Pause { get; set; }

        [ObservableProperty]
        [JsonProperty("slug"), JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

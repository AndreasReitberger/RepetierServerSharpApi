using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("SSID"), JsonPropertyName("SSID")]
        public partial string Ssid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("device"), JsonPropertyName("device")]
        public partial string Device { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("uuid"), JsonPropertyName("uuid")]
        public partial Guid Uuid { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

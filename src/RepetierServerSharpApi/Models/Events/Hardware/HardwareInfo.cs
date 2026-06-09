using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class HardwareInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("icon"), JsonPropertyName("icon")]
        public partial long? Icon { get; set; }

        [ObservableProperty]
        [JsonProperty("msgType"), JsonPropertyName("msgType")]
        public partial long? MsgType { get; set; }

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("text"), JsonPropertyName("text")]
        public partial string Text { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("unit"), JsonPropertyName("unit")]
        public partial string Unit { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("urgency"), JsonPropertyName("urgency")]
        public partial long? Urgency { get; set; }

        [ObservableProperty]
        [JsonProperty("url"), JsonPropertyName("url")]
        public partial string Url { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("value"), JsonPropertyName("value")]
        public partial double? Value { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

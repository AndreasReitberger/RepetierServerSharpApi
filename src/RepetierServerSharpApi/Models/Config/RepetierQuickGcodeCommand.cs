using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierQuickGcodeCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("command"), JsonPropertyName("command")]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("icon"), JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("visibleWhenPrinting"), JsonPropertyName("visibleWhenPrinting")]
        public partial bool VisibleWhenPrinting { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

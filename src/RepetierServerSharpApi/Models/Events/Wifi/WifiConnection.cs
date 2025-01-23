using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class WifiConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("SSID")]
        public partial string Ssid { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("device")]
        public partial string Device { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

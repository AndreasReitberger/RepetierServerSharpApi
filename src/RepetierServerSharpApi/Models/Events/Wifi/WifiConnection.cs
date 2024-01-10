using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class WifiConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("SSID")]
        [property: JsonIgnore]
        string ssid;

        [ObservableProperty]
        [JsonProperty("device")]
        [property: JsonIgnore]
        string device;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

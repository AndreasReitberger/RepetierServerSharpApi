using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class WifiConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("SSID")]

        string ssid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("device")]

        string device;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

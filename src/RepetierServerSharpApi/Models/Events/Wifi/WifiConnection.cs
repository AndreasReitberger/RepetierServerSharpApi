using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class WifiConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("SSID")]
        string ssid;

        [ObservableProperty]
        [JsonProperty("device")]
        string device;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

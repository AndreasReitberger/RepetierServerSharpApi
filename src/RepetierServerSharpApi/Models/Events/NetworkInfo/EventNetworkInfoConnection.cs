using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("SSID")]
        string ssid;

        [ObservableProperty]
        [JsonProperty("device")]
        string device;

        [ObservableProperty]
        [JsonProperty("uuid")]
        Guid uuid;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

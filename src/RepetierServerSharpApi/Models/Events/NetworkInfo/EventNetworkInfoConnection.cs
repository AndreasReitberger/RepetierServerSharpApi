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
        public partial string Ssid { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("device")]
        public partial string Device { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("uuid")]
        public partial Guid Uuid { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

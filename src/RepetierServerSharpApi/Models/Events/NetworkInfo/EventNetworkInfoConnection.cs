using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("SSID")]
        
        string ssid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("device")]
        
        string device;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("uuid")]
        
        Guid uuid;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

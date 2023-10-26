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
        [property: JsonIgnore]
        string ssid;

        [ObservableProperty]
        [JsonProperty("device")]
        [property: JsonIgnore]
        string device;

        [ObservableProperty]
        [JsonProperty("uuid")]
        [property: JsonIgnore]
        Guid uuid;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

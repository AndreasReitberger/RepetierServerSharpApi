using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        
        [JsonProperty("activeRouter")]
        public partial bool ActiveRouter { get; set; }

        [ObservableProperty]
        
        [JsonProperty("activeSSID")]
        public partial string ActiveSsid { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("apMode")]
        public partial long ApMode { get; set; }

        [ObservableProperty]
        
        [JsonProperty("apSSID")]
        public partial string ApSsid { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("channel")]
        public partial long Channel { get; set; }

        [ObservableProperty]
        
        [JsonProperty("channels")]
        public partial List<long> Channels { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("connections")]
        public partial List<EventNetworkInfoConnection> Connections { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("country")]
        public partial string Country { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("hostname")]
        public partial string Hostname { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("manageable")]
        public partial bool Manageable { get; set; }

        [ObservableProperty]
        
        [JsonProperty("mode")]
        public partial long Mode { get; set; }

        [ObservableProperty]
        
        [JsonProperty("routerList")]
        public partial List<EventNetworkInfoRouterList> RouterList { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("screensaver")]
        public partial bool Screensaver { get; set; }

        [ObservableProperty]
        
        [JsonProperty("timezone")]
        public partial string Timezone { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("version")]
        public partial long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

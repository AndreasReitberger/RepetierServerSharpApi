using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventWifiChangedData : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("activeRouter")]
        bool? activeRouter;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("activeSSID")]
        string activeSsid = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("apMode")]
        long? apMode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("apSSID")]
        string apSsid = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("channel")]
        long? channel;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("channels")]
        List<long> channels = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("connections")]

        List<WifiConnection> connections = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("country")]
        string country = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ethernet")]
        EthernetConnection? ethernet;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("hostname")]
        string hostname = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("manageable")]
        bool? manageable;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("manualWifi")]
        bool manualWifi;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("mode")]
        long? mode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("routerList")]
        List<RouterList> routerList = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("screensaver")]
        bool? screensaver;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("supportAP")]
        bool? supportAp;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timezone")]
        string timezone = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]
        long? version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

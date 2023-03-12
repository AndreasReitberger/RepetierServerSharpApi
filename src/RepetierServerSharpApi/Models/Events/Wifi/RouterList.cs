using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RouterList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("SSID")]
        string ssid;

        [ObservableProperty]
        [JsonProperty("active")]
        bool? active;

        [ObservableProperty]
        [JsonProperty("bars")]
        long? bars;

        [ObservableProperty]
        [JsonProperty("channel")]
        long? channel;

        [ObservableProperty]
        [JsonProperty("data")]
        ConnectionData data;

        [ObservableProperty]
        [JsonProperty("mode")]
        string mode;

        [ObservableProperty]
        [JsonProperty("rate")]
        string rate;

        [ObservableProperty]
        [JsonProperty("secure")]
        bool? secure;

        [ObservableProperty]
        [JsonProperty("signal")]
        long? signal;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

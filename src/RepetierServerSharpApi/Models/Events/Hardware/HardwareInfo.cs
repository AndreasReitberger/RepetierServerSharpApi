using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class HardwareInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("icon")]
        public long? icon;

        [ObservableProperty]
        [JsonProperty("msgType")]
        public long? msgType;

        [ObservableProperty]
        [JsonProperty("name")]
        public string name;

        [ObservableProperty]
        [JsonProperty("text")]
        public string text;

        [ObservableProperty]
        [JsonProperty("unit")]
        public string unit;

        [ObservableProperty]
        [JsonProperty("urgency")]
        public long? urgency;

        [ObservableProperty]
        [JsonProperty("url")]
        public string url;

        [ObservableProperty]
        [JsonProperty("value")]
        public double? value;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

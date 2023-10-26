using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class HardwareInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("icon")]
        [property: JsonIgnore]
        public long? icon;

        [ObservableProperty]
        [JsonProperty("msgType")]
        [property: JsonIgnore]
        public long? msgType;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        public string name;

        [ObservableProperty]
        [JsonProperty("text")]
        [property: JsonIgnore]
        public string text;

        [ObservableProperty]
        [JsonProperty("unit")]
        [property: JsonIgnore]
        public string unit;

        [ObservableProperty]
        [JsonProperty("urgency")]
        [property: JsonIgnore]
        public long? urgency;

        [ObservableProperty]
        [JsonProperty("url")]
        [property: JsonIgnore]
        public string url;

        [ObservableProperty]
        [JsonProperty("value")]
        [property: JsonIgnore]
        public double? value;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

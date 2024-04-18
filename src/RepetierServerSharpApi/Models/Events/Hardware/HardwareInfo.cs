using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class HardwareInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("icon")]
        public long? icon;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("msgType")]
        public long? msgType;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        public string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("text")]
        public string text;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("unit")]
        public string unit;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("urgency")]
        public long? urgency;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("url")]
        public string url;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("value")]
        public double? value;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

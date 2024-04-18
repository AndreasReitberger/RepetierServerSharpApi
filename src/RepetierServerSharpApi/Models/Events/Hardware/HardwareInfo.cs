using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class HardwareInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("icon")]
        long? icon;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("msgType")]
        long? msgType;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("text")]
        string text = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("unit")]
        string unit = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("urgency")]
        long? urgency;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("url")]
        string url = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("value")]
        double? value;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

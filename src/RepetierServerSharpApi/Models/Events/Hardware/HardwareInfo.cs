using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class HardwareInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        
        [JsonProperty("icon")]
        public partial long? Icon { get; set; }

        [ObservableProperty]
        
        [JsonProperty("msgType")]
        public partial long? MsgType { get; set; }

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("text")]
        public partial string Text { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("unit")]
        public partial string Unit { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("urgency")]
        public partial long? Urgency { get; set; }

        [ObservableProperty]
        
        [JsonProperty("url")]
        public partial string Url { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("value")]
        public partial double? Value { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

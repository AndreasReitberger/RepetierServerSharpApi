using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierQuickGcodeCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("command")]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("visibleWhenPrinting")]
        public partial bool VisibleWhenPrinting { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

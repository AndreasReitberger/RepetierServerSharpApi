using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierLicenseInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        
        [JsonProperty("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        
        [JsonProperty("hasBranding")]
        public partial bool HasBranding { get; set; }

        [ObservableProperty]
        
        [JsonProperty("licence")]
        public partial string Licence { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("wantsBranding")]
        public partial bool WantsBranding { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

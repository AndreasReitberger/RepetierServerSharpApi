using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierLicenseInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("active"), JsonPropertyName("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        [JsonProperty("hasBranding"), JsonPropertyName("hasBranding")]
        public partial bool HasBranding { get; set; }

        [ObservableProperty]
        [JsonProperty("licence"), JsonPropertyName("licence")]
        public partial string Licence { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("wantsBranding"), JsonPropertyName("wantsBranding")]
        public partial bool WantsBranding { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

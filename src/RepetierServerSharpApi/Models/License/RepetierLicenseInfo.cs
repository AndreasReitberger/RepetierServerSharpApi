using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierLicenseInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("active")]
        [property: JsonIgnore]
        bool active;

        [ObservableProperty]
        [JsonProperty("hasBranding")]
        [property: JsonIgnore]
        bool hasBranding;

        [ObservableProperty]
        [JsonProperty("licence")]
        [property: JsonIgnore]
        string licence;

        [ObservableProperty]
        [JsonProperty("wantsBranding")]
        [property: JsonIgnore]
        bool wantsBranding;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

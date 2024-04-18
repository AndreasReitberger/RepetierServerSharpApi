using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierLicenseInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("active")]
        bool active;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("hasBranding")]
        bool hasBranding;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("licence")]
        string licence;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("wantsBranding")]
        bool wantsBranding;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

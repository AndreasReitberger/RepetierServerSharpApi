using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierLicenseInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("active")]
        bool active;

        [ObservableProperty]
        [JsonProperty("hasBranding")]
        bool hasBranding;

        [ObservableProperty]
        [JsonProperty("licence")]
        string licence;

        [ObservableProperty]
        [JsonProperty("wantsBranding")]
        bool wantsBranding;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }

}

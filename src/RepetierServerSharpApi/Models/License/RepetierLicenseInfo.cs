using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierLicenseInfo
    {
        #region Properties
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("hasBranding")]
        public bool HasBranding { get; set; }

        [JsonProperty("licence")]
        public string Licence { get; set; }

        [JsonProperty("wantsBranding")]
        public bool WantsBranding { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }

}

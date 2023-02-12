using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResult
    {
        #region Properties
        [JsonProperty("login", NullValueHandling = NullValueHandling.Ignore)]
        public string Login { get; set; }

        [JsonProperty("permissions", NullValueHandling = NullValueHandling.Ignore)]
        public long? Permissions { get; set; }

        [JsonProperty("settings", NullValueHandling = NullValueHandling.Ignore)]
        public RepetierLoginResultSettings Settings { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsData
    {
        #region Properties
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("permissions")]
        public long Permissions { get; set; }

        [JsonProperty("settings")]
        public EventUserCredentialsSettings Settings { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

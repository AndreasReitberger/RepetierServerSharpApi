using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentials
    {
        #region Properties

        [JsonProperty("data")]
        public EventUserCredentialsData Data { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

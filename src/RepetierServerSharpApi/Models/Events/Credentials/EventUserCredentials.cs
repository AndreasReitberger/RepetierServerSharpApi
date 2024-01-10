using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentials : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        EventUserCredentialsData data;

        [ObservableProperty]
        [JsonProperty("event")]
        [property: JsonIgnore]
        string eventName;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

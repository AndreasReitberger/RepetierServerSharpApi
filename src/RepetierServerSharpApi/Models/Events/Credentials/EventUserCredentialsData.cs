using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("login")]
        [property: JsonIgnore]
        string login;

        [ObservableProperty]
        [JsonProperty("permissions")]
        [property: JsonIgnore]
        long permissions;

        [ObservableProperty]
        [JsonProperty("settings")]
        [property: JsonIgnore]
        EventUserCredentialsSettings settings;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

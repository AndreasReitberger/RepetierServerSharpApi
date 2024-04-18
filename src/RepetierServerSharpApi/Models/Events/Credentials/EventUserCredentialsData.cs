using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsData : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("login")]
        string login;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("permissions")]
        long permissions;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("settings")]
        EventUserCredentialsSettings settings;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

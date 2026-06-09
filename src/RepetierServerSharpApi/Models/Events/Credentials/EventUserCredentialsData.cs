using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("login"), JsonPropertyName("login")]
        public partial string Login { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("permissions"), JsonPropertyName("permissions")]
        public partial long Permissions { get; set; }

        [ObservableProperty]
        [JsonProperty("settings"), JsonPropertyName("settings")]
        public partial EventUserCredentialsSettings? Settings { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

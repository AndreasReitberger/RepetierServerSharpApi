using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("login")]
        public partial string Login { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("permissions")]
        public partial long Permissions { get; set; }

        [ObservableProperty]
        
        [JsonProperty("settings")]
        public partial EventUserCredentialsSettings? Settings { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

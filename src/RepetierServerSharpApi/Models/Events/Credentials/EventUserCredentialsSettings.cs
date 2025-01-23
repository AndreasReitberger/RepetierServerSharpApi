using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("gcodeGroup")]
        public partial string GcodeGroup { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("gcodeSortBy")]
        public partial long GcodeSortBy { get; set; }

        [ObservableProperty]

        [JsonProperty("gcodeViewMode")]
        public partial long GcodeViewMode { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

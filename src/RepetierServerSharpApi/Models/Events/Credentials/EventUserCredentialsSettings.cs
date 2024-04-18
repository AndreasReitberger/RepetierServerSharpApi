using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gcodeGroup")]
        string gcodeGroup = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gcodeSortBy")]
        long gcodeSortBy;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gcodeViewMode")]
        long gcodeViewMode;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

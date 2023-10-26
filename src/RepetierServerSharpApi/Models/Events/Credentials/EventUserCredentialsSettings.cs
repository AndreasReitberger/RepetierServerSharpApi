using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("gcodeGroup")]
        [property: JsonIgnore]
        string gcodeGroup;

        [ObservableProperty]
        [JsonProperty("gcodeSortBy")]
        [property: JsonIgnore]
        long gcodeSortBy;

        [ObservableProperty]
        [JsonProperty("gcodeViewMode")]
        [property: JsonIgnore]
        long gcodeViewMode;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        EventNetworkInfoData data;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

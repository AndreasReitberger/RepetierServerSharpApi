using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTempData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("O")]
        [property: JsonIgnore]
        long o;

        [ObservableProperty]
        [JsonProperty("S")]
        [property: JsonIgnore]
        long s;

        [ObservableProperty]
        [JsonProperty("T")]
        [property: JsonIgnore]
        double t;

        [ObservableProperty]
        [JsonProperty("id")]
        [property: JsonIgnore]
        long id;

        [ObservableProperty]
        [JsonProperty("t")]
        [property: JsonIgnore]
        long dataT;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

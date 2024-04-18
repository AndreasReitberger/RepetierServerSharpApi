using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTempData : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("O")]
        long o;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("S")]
        long s;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("T")]
        double t;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("id")]
        long id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("t")]
        long dataT;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

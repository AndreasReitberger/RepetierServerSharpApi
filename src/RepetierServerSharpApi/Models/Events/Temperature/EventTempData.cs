using CommunityToolkit.Mvvm.ComponentModel;
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
        long id;

        [ObservableProperty]
        [JsonProperty("t")]
        long dataT;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        /*
        public override string ToString()
        {
            return $"S: {S}, T: {T}, O: {O} ({Id})";
        }*/
        #endregion
    }
}

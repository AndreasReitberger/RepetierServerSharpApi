using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierFreeSpaceRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("available")]

        long available;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("capacity")]

        long capacity;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("free")]

        long free;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

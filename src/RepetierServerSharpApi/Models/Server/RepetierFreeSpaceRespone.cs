using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierFreeSpaceRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("available")]
        [property: JsonIgnore]
        long available;

        [ObservableProperty]
        [JsonProperty("capacity")]
        [property: JsonIgnore]
        long capacity;

        [ObservableProperty]
        [JsonProperty("free")]
        [property: JsonIgnore]
        long free;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

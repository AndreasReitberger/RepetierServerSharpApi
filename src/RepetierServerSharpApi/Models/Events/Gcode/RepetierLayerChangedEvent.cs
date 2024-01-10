using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLayerChangedEvent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("layer")]
        [property: JsonIgnore]
        long layer;

        [ObservableProperty]
        [JsonProperty("maxLayer")]
        [property: JsonIgnore]
        long maxLayer;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

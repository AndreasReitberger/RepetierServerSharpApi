using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventDispatcherCountChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("count")]
        [property: JsonIgnore]
        long count;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

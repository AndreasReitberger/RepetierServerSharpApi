using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobStartedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("start")]
        [property: JsonIgnore]
        long? start;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

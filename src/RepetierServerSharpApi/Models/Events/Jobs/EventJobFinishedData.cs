using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobFinishedData : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("duration")]
        long? duration;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("end")]
        long? end;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("lines")]
        long? lines;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("start")]
        long? start;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

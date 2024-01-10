using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobFinishedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("duration")]
        [property: JsonIgnore]
        long? duration;

        [ObservableProperty]
        [JsonProperty("end")]
        [property: JsonIgnore]
        long? end;

        [ObservableProperty]
        [JsonProperty("lines")]
        [property: JsonIgnore]
        long? lines;

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

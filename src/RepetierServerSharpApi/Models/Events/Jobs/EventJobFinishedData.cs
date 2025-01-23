using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobFinishedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]

        [JsonProperty("duration")]
        public partial long? Duration { get; set; }

        [ObservableProperty]

        [JsonProperty("end")]
        public partial long? End { get; set; }

        [ObservableProperty]

        [JsonProperty("lines")]
        public partial long? Lines { get; set; }

        [ObservableProperty]

        [JsonProperty("start")]
        public partial long? Start { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class EventJobFinishedData
    {
        #region Properties
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public long? Duration { get; set; }

        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public long? End { get; set; }

        [JsonProperty("lines", NullValueHandling = NullValueHandling.Ignore)]
        public long? Lines { get; set; }

        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public long? Start { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

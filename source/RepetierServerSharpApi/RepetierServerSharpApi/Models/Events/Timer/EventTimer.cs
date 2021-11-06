using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class EventTimer
    {
        #region Properties
        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

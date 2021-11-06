using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierEventContainer
    {
        #region Properties
        [JsonProperty("callback_id")]
        public long CallbackId { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierEventData> Data { get; set; } = new();

        [JsonProperty("eventList")]
        public bool EventList { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

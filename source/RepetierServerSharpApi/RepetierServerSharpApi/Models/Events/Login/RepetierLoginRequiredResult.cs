using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierLoginRequiredResult
    {
        #region Properties
        [JsonProperty("callback_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? CallbackId { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierLoginRequiredResultData> Data { get; set; } = new();

        [JsonProperty("eventList", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EventList { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

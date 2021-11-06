using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class EventPrinterListChanged
    {
        #region Properties
        [JsonProperty("data")]
        public List<EventPrinterListChangedData> Data { get; set; } = new();

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

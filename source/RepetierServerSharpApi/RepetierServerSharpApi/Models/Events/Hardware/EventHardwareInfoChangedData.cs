using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventHardwareInfoChangedData
    {
        #region Properties
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public List<HardwareInfo> List { get; set; } = new();

        [JsonProperty("maxUrgency", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxUrgency { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

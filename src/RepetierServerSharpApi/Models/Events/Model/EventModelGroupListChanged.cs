using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventModelGroupListChanged
    {
        #region Properties
        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("printer")]
        public string Printer { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierEventData
    {
        #region Properties
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }

        [JsonProperty("event", NullValueHandling = NullValueHandling.Ignore)]
        public string Event { get; set; }

        [JsonProperty("printer", NullValueHandling = NullValueHandling.Ignore)]
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

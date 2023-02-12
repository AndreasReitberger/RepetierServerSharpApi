using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobStartedData
    {
        #region Properties
        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public long? Start { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventDispatcherCountChangedData
    {
        #region Properties
        [JsonProperty("count")]
        public long Count { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.Models
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
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

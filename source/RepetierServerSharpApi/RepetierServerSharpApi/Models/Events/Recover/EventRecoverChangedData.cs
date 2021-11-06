using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class EventRecoverChangedData
    {
        #region Properties
        [JsonProperty("state")]
        public long State { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

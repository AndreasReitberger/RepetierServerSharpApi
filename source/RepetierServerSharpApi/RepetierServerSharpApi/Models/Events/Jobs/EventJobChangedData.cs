using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class EventJobChangedData
    {
        #region Properties
        [JsonProperty("slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }

}

using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierLoginRequiredResultDataItem
    {
        #region Properties
        [JsonProperty("session", NullValueHandling = NullValueHandling.Ignore)]
        public string Session { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

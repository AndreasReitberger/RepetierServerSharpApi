using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierActionResult
    {
        #region Properties
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

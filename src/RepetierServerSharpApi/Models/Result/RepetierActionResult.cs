using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
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
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

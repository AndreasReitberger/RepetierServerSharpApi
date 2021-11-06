using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierModelGroup
    {
        #region Properties
        [JsonProperty("groupNames")]
        public string[] GroupNames { get; set; }

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

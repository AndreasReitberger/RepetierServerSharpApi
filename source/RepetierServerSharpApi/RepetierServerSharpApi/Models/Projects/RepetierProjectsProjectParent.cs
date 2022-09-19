using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectParent
    {
        #region Properties
        [JsonProperty("empty", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Empty { get; set; }

        [JsonProperty("idx", NullValueHandling = NullValueHandling.Ignore)]
        public long? Idx { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

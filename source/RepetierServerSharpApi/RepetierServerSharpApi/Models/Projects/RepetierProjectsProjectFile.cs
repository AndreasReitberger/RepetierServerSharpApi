using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierProjectsProjectFile
    {
        #region Properties
        [JsonProperty("n", NullValueHandling = NullValueHandling.Ignore)]
        public string N { get; set; }

        [JsonProperty("s", NullValueHandling = NullValueHandling.Ignore)]
        public long? S { get; set; }

        [JsonProperty("p", NullValueHandling = NullValueHandling.Ignore)]
        public string P { get; set; }
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

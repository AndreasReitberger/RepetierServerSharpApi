using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectRespone
    {
        #region Properties
        [JsonProperty("ok", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Ok { get; set; }

        [JsonProperty("project", NullValueHandling = NullValueHandling.Ignore)]
        public RepetierProjectsProject Project { get; set; }
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

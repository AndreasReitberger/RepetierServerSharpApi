using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierProjectsServerListRespone
    {
        #region Properties
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("server")]
        public List<ProjectsServer> Server { get; set; } = new List<ProjectsServer>();
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }

}

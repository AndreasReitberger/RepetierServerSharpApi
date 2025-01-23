using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsServerListRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("ok")]
        public partial bool Ok { get; set; }

        [ObservableProperty]

        [JsonProperty("server")]
        public partial List<ProjectsServer> Server { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

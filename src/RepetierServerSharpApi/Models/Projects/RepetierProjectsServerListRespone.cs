using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsServerListRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("ok")]
        [property: JsonIgnore]
        bool ok;

        [ObservableProperty]
        [JsonProperty("server")]
        [property: JsonIgnore]
        List<ProjectsServer> server = new();
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsServerListRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("ok")]
        bool ok;

        [ObservableProperty]
        [JsonProperty("server")]
        List<ProjectsServer> server = new();
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }

}

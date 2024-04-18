using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ProjectsServer : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]

        string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("uuid")]

        Guid uuid;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

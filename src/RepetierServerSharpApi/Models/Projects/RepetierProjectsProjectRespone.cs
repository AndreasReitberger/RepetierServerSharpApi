using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ok")]

        bool? ok;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("project")]

        RepetierProjectsProject project;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

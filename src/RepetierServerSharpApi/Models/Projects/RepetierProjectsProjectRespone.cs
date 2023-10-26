using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("ok")]
        [property: JsonIgnore]
        bool? ok;

        [ObservableProperty]
        [JsonProperty("project")]
        [property: JsonIgnore]
        RepetierProjectsProject project;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

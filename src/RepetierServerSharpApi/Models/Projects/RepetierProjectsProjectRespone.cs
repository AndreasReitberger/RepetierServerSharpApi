using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("ok")]
        public partial bool? Ok { get; set; }

        [ObservableProperty]
        
        [JsonProperty("project")]
        public partial RepetierProjectsProject? Project { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

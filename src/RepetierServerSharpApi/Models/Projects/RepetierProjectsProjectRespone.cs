using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("ok"), JsonPropertyName("ok")]
        public partial bool? Ok { get; set; }

        [ObservableProperty]
        [JsonProperty("project"), JsonPropertyName("project")]
        public partial RepetierProjectsProject? Project { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

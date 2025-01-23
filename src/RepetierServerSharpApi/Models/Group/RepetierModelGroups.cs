using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelGroups : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("groupNames")]
        public partial string[] GroupNames { get; set; } = [];

        [JsonProperty("ok")]
        [ObservableProperty]

        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

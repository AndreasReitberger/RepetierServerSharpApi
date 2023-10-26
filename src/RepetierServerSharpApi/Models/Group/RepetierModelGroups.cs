using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelGroups : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("groupNames")]
        [property: JsonIgnore]
        string[] groupNames;

        [JsonProperty("ok")]
        [ObservableProperty]
        [property: JsonIgnore]
        bool ok;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

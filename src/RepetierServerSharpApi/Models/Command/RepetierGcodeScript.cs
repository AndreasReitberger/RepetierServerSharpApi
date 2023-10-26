using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGcodeScript : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty,JsonProperty("script")]
        [property: JsonIgnore]
        string script;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

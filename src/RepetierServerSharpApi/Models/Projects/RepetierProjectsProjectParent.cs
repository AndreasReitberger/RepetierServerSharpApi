using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectParent : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("empty")]

        bool? empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("idx")]

        long? idx;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]

        string name;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

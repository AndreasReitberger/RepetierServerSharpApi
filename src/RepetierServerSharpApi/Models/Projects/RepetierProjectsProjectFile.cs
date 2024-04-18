using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectFile : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("n")]

        string n;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("s")]

        long? s;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("p")]

        string p;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

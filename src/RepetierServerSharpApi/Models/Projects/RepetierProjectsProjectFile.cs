using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectFile : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("n")]
        [property: JsonIgnore]
        string n;

        [ObservableProperty]
        [JsonProperty("s")]
        [property: JsonIgnore]
        long? s;

        [ObservableProperty]
        [JsonProperty("p")]
        [property: JsonIgnore]
        string p;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

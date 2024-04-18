using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectComment : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("comment")]
        string comment = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("time")]
        long? time;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("user")]
        string user = string.Empty;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

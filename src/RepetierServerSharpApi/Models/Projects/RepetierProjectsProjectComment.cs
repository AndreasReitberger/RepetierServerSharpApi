using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectComment : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("comment")]
        [property: JsonIgnore]
        string comment;

        [ObservableProperty]
        [JsonProperty("time")]
        [property: JsonIgnore]
        long? time;

        [ObservableProperty]
        [JsonProperty("user")]
        [property: JsonIgnore]
        string user;
        #endregion 

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

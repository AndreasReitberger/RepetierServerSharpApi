using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectParentElement : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("empty")]
        [property: JsonIgnore]
        bool empty;

        [ObservableProperty]
        [JsonProperty("idx")]
        [property: JsonIgnore]
        long idx;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

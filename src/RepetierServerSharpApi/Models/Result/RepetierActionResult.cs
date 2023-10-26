using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierActionResult : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("ok")]
        [property: JsonIgnore]
        bool ok;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

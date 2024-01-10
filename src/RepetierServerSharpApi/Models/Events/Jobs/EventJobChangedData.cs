using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobChangedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }

}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginRequiredResultDataItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("session")]
        [property: JsonIgnore]
        string session;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

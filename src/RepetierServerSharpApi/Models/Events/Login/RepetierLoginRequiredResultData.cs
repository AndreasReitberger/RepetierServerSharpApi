using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginRequiredResultData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        RepetierLoginRequiredResultDataItem data;

        [ObservableProperty]
        [JsonProperty("event")]
        [property: JsonIgnore]
        string eventName;

        [ObservableProperty]
        [JsonProperty("printer")]
        [property: JsonIgnore]
        string printer;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }

}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGcodeReplacement : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("comment")]
        [property: JsonIgnore]
        string comment;

        [ObservableProperty]
        [JsonProperty("expression")]
        [property: JsonIgnore]
        string expression;

        [ObservableProperty]
        [JsonProperty("script")]
        [property: JsonIgnore]
        string script;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }

}

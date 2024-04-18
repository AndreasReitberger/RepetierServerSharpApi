using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGcodeReplacement : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("comment")]
        string comment = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("expression")]
        string expression = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("script")]
        string script = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }

}

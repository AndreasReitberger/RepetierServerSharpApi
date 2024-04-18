using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigTemperature : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("temp")]
        long temp;

        #region Json Ignore
        [ObservableProperty, JsonIgnore]
        string targetComponent;
        #endregion

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

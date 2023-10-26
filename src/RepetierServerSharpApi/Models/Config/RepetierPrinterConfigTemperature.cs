using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigTemperature : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("temp")]
        [property: JsonIgnore]
        long temp;

        #region Json Ignore
        [ObservableProperty]
        [JsonIgnore]
        string targetComponent;
        #endregion

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

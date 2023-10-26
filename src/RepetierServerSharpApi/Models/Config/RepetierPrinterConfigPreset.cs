using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigPreset : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("value")]
        [property: JsonIgnore]
        int value;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

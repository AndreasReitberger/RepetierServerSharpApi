using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigButtonCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("command")]
        [property: JsonIgnore]
        string command;

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

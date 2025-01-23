using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigButtonCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("command")]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigTemperature : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("temp")]
        public partial long Temp { get; set; }

        #region Json Ignore
        [ObservableProperty]

        public partial string TargetComponent { get; set; } = string.Empty;
        #endregion

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

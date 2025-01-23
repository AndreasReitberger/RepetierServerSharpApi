using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGcodeReplacement : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("comment")]
        public partial string Comment { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("expression")]
        public partial string Expression { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("script")]
        public partial string Script { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }

}

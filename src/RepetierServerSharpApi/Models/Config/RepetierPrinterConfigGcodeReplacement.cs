using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGcodeReplacement : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("comment"), JsonPropertyName("comment")]
        public partial string Comment { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("expression"), JsonPropertyName("expression")]
        public partial string Expression { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("script"), JsonPropertyName("script")]
        public partial string Script { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }

}

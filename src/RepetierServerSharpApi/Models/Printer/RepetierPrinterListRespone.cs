using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterListRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("data"), JsonPropertyName("data")]
        public partial List<RepetierPrinter> Printers { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

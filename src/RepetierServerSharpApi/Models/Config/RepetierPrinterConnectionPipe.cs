using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionPipe : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("file"), JsonPropertyName("file")]
        public partial string File { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

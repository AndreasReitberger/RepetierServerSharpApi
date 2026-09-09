using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionIp : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("address"), JsonPropertyName("address")]
        public partial string Address { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("keepAliveInterval"), JsonPropertyName("keepAliveInterval")]
        public partial long KeepAliveInterval { get; set; }

        [ObservableProperty]
        [JsonProperty("port"), JsonPropertyName("port")]
        public partial long Port { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }

}

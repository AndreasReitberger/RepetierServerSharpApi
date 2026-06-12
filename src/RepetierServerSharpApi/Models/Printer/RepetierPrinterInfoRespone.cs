using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterInfoRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("printers"), JsonPropertyName("printers")]
        public partial List<RepetierPrinterInfo> Printers { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("servername"), JsonPropertyName("servername")]
        public partial string Servername { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("serveruuid"), JsonPropertyName("serveruuid")]
        public partial Guid Serveruuid { get; set; }

        [ObservableProperty]
        [JsonProperty("version"), JsonPropertyName("version")]
        public partial string Version { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

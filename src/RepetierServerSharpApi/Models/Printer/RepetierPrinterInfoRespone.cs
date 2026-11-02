using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterInfoRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("printers")]
        public partial List<RepetierPrinterInfo> Printers { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("servername")]
        public partial string Servername { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("serveruuid")]
        public partial Guid Serveruuid { get; set; }

        [ObservableProperty]
        [JsonPropertyName("version")]
        public partial string Version { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterInfoRespone);
        #endregion
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterInfoRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]

        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("printers")]
        public partial List<RepetierPrinterInfo> Printers { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("servername")]
        public partial string Servername { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("serveruuid")]
        public partial Guid Serveruuid { get; set; }

        [ObservableProperty]

        [JsonProperty("version")]
        public partial string Version { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

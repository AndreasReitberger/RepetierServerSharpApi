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
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("printers")]
        [property: JsonIgnore]
        List<RepetierPrinterInfo> printers = new();

        [ObservableProperty]
        [JsonProperty("servername")]
        [property: JsonIgnore]
        string servername;

        [ObservableProperty]
        [JsonProperty("serveruuid")]
        [property: JsonIgnore]
        Guid serveruuid;

        [ObservableProperty]
        [JsonProperty("version")]
        [property: JsonIgnore]
        string version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

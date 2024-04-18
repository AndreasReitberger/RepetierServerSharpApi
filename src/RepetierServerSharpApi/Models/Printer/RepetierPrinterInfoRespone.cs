using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterInfoRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]

        string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printers")]

        List<RepetierPrinterInfo> printers = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("servername")]

        string servername;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("serveruuid")]

        Guid serveruuid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]

        string version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

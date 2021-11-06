using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterInfoRespone
    {
        #region Properties
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("printers")]
        public List<RepetierPrinterInfo> Printers { get; set; } = new();

        [JsonProperty("servername")]
        public string Servername { get; set; }

        [JsonProperty("serveruuid")]
        public Guid Serveruuid { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

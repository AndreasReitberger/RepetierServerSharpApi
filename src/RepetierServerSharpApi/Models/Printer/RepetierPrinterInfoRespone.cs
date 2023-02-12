using CommunityToolkit.Mvvm.ComponentModel;
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
        string name;

        [ObservableProperty]
        [JsonProperty("printers")]
        List<RepetierPrinterInfo> printers = new();

        [ObservableProperty]
        [JsonProperty("servername")]
        string servername;

        [ObservableProperty]
        [JsonProperty("serveruuid")]
        Guid serveruuid;

        [ObservableProperty]
        [JsonProperty("version")]
        string version;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

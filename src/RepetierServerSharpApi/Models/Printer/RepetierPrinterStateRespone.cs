using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterStateRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("printer")]
        RepetierPrinterState printer = new();

        [ObservableProperty]
        [JsonProperty("printer1")]
        RepetierPrinterState printer1 = new();
        
        //[ObservableProperty]
        //Dictionary<string, RepetierPrinterState> printers = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

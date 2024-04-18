using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Don't use this anymore")]
    public partial class RepetierPrinterStateRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printer")]
        RepetierPrinterState printer = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printer1")]
        RepetierPrinterState printer1 = new();

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGpioListRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("list")]
        List<RepetierGpioListItem> list = new();

        [ObservableProperty]
        [JsonProperty("ok")]
        bool ok;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCallList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("list")]
        List<RepetierWebCallAction> list = new();

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

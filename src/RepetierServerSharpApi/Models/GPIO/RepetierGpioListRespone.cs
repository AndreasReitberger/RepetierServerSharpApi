using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGpioListRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("list")]
        public partial List<RepetierGpioListItem> List { get; set; } = new();

        [ObservableProperty]
        
        [JsonProperty("ok")]
        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

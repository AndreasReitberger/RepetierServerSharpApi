using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCallList : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("list")]
        public partial List<RepetierWebCallAction> List { get; set; } = new();

        [ObservableProperty]

        [JsonProperty("ok")]
        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

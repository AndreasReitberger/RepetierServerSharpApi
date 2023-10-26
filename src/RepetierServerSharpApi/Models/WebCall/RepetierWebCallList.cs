using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCallList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("list")]
        [property: JsonIgnore]
        List<RepetierWebCallAction> list = new();

        [ObservableProperty]
        [JsonProperty("ok")]
        [property: JsonIgnore]
        bool ok;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

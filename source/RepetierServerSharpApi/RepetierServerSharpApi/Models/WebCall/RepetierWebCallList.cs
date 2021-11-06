using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierWebCallList
    {
        #region Properties
        [JsonProperty("list")]
        public List<RepetierWebCallAction> List { get; set; } = new();

        [JsonProperty("ok")]
        public bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

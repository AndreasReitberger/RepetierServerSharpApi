using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierHistoryListRespone
    {
        #region Properties
        [JsonProperty("list")]
        public List<RepetierHistoryListItem> List { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

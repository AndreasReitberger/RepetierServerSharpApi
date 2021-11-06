using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierHistorySummaryRespone
    {
        #region Properties
        [JsonProperty("list")]
        public List<RepetierHistorySummaryItem> Summaries { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }

}

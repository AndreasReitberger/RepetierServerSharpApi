using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistorySummaryRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        
        [JsonProperty("list")]
        public partial List<RepetierHistorySummaryItem> Summaries { get; set; } = new();

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

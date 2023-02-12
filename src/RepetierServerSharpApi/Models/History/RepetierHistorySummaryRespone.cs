using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistorySummaryRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("list")]
        List<RepetierHistorySummaryItem> summaries = new();

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }

}

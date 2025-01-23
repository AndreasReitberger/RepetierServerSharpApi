using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChanged : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("data")]
        public partial List<EventPrinterListChangedData> Data { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("event")]
        public partial string EventName { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

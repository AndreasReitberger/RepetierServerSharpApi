using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChanged : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        List<EventPrinterListChangedData> data = new();

        [ObservableProperty]
        [JsonProperty("event")]
        [property: JsonIgnore]
        string eventName;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

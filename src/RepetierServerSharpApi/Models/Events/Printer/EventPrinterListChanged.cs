using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChanged : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("data")]

        List<EventPrinterListChangedData> data = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("event")]

        string eventName;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

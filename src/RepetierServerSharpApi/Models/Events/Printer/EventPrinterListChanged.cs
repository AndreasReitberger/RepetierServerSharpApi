using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChanged : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("data")]
        List<EventPrinterListChangedData> data = new();

        [ObservableProperty]
        [JsonProperty("event")]
        string eventName;
        //string @event;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

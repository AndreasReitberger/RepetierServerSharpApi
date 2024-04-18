using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierEventContainer : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("callback_id")]

        long callbackId;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("data")]

        List<RepetierEventData> data = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("eventList")]

        bool eventList;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

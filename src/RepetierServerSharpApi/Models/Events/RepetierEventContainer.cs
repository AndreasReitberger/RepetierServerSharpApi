using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierEventContainer : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("callback_id")]
        [property: JsonIgnore]
        long callbackId;

        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        List<RepetierEventData> data = new();

        [ObservableProperty]
        [JsonProperty("eventList")]
        [property: JsonIgnore]
        bool eventList;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginRequiredResult : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("callback_id")]
        long? callbackId;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("data")]
        List<RepetierLoginRequiredResultData> data = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("eventList")]
        bool? eventList;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

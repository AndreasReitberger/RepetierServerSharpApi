using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginRequiredResult : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("callback_id")]
        [property: JsonIgnore]
        long? callbackId;

        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        List<RepetierLoginRequiredResultData> data = new();

        [ObservableProperty]
        [JsonProperty("eventList")]
        [property: JsonIgnore]
        bool? eventList;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginRequiredResult : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("callback_id")]
        public partial long? CallbackId { get; set; }

        [ObservableProperty]
        
        [JsonProperty("data")]
        public partial List<RepetierLoginRequiredResultData> Data { get; set; } = new();

        [ObservableProperty]
        
        [JsonProperty("eventList")]
        public partial bool? EventList { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

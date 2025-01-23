using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierEventContainer : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        
        [JsonProperty("callback_id")]
        public partial long CallbackId { get; set; }

        [ObservableProperty]
        
        [JsonProperty("data")]
        public partial List<RepetierEventData> Data { get; set; } = new();

        [ObservableProperty]
        
        [JsonProperty("eventList")]
        public partial bool EventList { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

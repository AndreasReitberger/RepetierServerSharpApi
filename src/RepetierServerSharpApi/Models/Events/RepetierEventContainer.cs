using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierEventContainer : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("callback_id"), JsonPropertyName("callback_id")]
        public partial long CallbackId { get; set; }

        [ObservableProperty]
        [JsonProperty("data"), JsonPropertyName("data")]
        public partial List<RepetierEventData> Data { get; set; } = new();

        [ObservableProperty]
        [JsonProperty("eventList"), JsonPropertyName("eventList")]
        public partial bool EventList { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

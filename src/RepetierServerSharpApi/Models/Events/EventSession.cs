using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventSession : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("callback_id")]
        public partial long CallbackId { get; set; }

        [ObservableProperty]

        [JsonProperty("data")]
        public partial object? Data { get; set; }

        [ObservableProperty]

        [JsonProperty("session")]
        public partial string Session { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

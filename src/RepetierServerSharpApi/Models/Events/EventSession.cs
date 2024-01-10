using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventSession : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("callback_id")]
        [property: JsonIgnore]
        long callbackId;

        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        object data;

        [ObservableProperty]
        [JsonProperty("session")]
        [property: JsonIgnore]
        string session;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventSession : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("callback_id")]
        long callbackId;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("data")]
        object? data;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("session")]
        string session = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

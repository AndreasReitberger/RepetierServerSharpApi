using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventDispatcherCountChanged : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("data")]
        public partial EventDispatcherCountChangedData? Data { get; set; }

        [ObservableProperty]
        
        [JsonProperty("event")]
        public partial string EventName { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierEventData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("data")]
        public partial object? Data { get; set; }

        [ObservableProperty]
        
        [JsonProperty("event")]
        public partial string EventName { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("printer")]
        public partial string Printer { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

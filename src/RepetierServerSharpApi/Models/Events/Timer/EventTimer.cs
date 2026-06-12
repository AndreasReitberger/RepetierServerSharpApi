using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTimer : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("data"), JsonPropertyName("data")]
        public partial object? Data { get; set; }

        [ObservableProperty]
        [JsonProperty("event"), JsonPropertyName("event")]
        public partial string? EventName { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

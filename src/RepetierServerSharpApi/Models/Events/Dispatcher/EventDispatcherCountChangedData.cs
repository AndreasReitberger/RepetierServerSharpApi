using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventDispatcherCountChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("count"), JsonPropertyName("count")]
        public partial long Count { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobStartedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("start"), JsonPropertyName("start")]
        public partial long? Start { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

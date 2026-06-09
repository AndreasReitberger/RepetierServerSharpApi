using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventRecoverChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("state"), JsonPropertyName("state")]
        public partial long State { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

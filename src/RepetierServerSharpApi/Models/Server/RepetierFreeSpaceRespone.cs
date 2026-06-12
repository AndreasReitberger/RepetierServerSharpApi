using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierFreeSpaceRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("available"), JsonPropertyName("available")]
        public partial long Available { get; set; }

        [ObservableProperty]
        [JsonProperty("capacity"), JsonPropertyName("capacity")]
        public partial long Capacity { get; set; }

        [ObservableProperty]
        [JsonProperty("free"), JsonPropertyName("free")]
        public partial long Free { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

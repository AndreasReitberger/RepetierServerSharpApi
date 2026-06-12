using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierUpdateTeaser : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("available"), JsonPropertyName("available")]
        public partial bool Available { get; set; }

        [ObservableProperty]
        [JsonProperty("end"), JsonPropertyName("end")]
        public partial long End { get; set; }

        [ObservableProperty]
        [JsonProperty("msg"), JsonPropertyName("msg")]
        public partial string Msg { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("start"), JsonPropertyName("start")]
        public partial long Start { get; set; }

        [ObservableProperty]
        [JsonProperty("updated"), JsonPropertyName("updated")]
        public partial long Updated { get; set; }

        [ObservableProperty]
        [JsonProperty("url"), JsonPropertyName("url")]
        public partial Uri? Url { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

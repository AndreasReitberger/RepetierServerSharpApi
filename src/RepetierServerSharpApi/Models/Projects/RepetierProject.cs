using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProject : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("folder"), JsonPropertyName("folder")]
        public partial long Folder { get; set; }

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("preview"), JsonPropertyName("preview")]
        public partial string Preview { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("uuid"), JsonPropertyName("uuid")]
        public partial Guid Uuid { get; set; }

        [ObservableProperty]
        [JsonProperty("version"), JsonPropertyName("version")]
        public partial long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCallAction : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("content_type"), JsonPropertyName("content_type")]
        public partial string ContentType { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("icon"), JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("method"), JsonPropertyName("method")]
        public partial string Method { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("pos"), JsonPropertyName("pos")]
        public partial long Pos { get; set; }

        [ObservableProperty]
        [JsonProperty("post"), JsonPropertyName("post")]
        public partial string Post { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("question"), JsonPropertyName("question") ]
        public partial string Question { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("show_in_menu"), JsonPropertyName("show_in_menu")]
        public partial bool ShowInMenu { get; set; }

        [ObservableProperty]
        [JsonProperty("show_name"), JsonPropertyName("show_name")]
        public partial string ShowName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("slug"), JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("url"), JsonPropertyName("url")]
        public partial Uri? Url { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

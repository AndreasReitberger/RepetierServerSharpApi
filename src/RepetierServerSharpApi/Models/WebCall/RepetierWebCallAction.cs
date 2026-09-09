using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCallAction : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("content_type")]
        public partial string ContentType { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [J  ("method")]
        public partial string Method { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("pos")]
        public partial long Pos { get; set; }

        [ObservableProperty]
        [JsonPropertyName("post")]
        public partial string Post { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("question")]
        public partial string Question { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("show_in_menu")]
        public partial bool ShowInMenu { get; set; }

        [ObservableProperty]
        [JsonPropertyName("show_name")]
        public partial string ShowName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("url")]
        public partial Uri? Url { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this, RepetierSourceGenerationContext.Default.RepetierWebCallAction);
        #endregion
    }
}

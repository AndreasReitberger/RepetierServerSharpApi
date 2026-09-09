using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventMessageChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("date")]
        public partial DateTimeOffset? Date { get; set; }

        [ObservableProperty]
        [JsonPropertyName("id")]
        public partial long? Id { get; set; }

        [ObservableProperty]
        [JsonPropertyName("link")]
        public partial string Link { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("msg")]
        public partial string Msg { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("pause")]
        public partial bool? Pause { get; set; }

        [ObservableProperty]
        [JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventMessageChangedData);
        #endregion
    }
}

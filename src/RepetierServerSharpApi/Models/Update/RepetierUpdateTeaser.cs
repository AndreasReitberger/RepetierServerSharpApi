using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierUpdateTeaser : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("available")]
        public partial bool Available { get; set; }

        [ObservableProperty]
        [JsonPropertyName("end")]
        public partial long End { get; set; }

        [ObservableProperty]
        [JsonPropertyName("msg")]
        public partial string Msg { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("start")]
        public partial long Start { get; set; }

        [ObservableProperty]
        [JsonPropertyName("updated")]
        public partial long Updated { get; set; }

        [ObservableProperty]
        [JsonPropertyName("url")]
        public partial Uri? Url { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this, RepetierSourceGenerationContext.Default.RepetierUpdateTeaser);
        #endregion
    }

}

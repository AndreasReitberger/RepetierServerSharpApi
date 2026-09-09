using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProject : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("folder")]
        public partial long Folder { get; set; }

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("preview")]
        public partial string Preview { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("uuid")]
        public partial Guid Uuid { get; set; }

        [ObservableProperty]
        [JsonPropertyName("version")]
        public partial long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierProject);
        #endregion
    }
}

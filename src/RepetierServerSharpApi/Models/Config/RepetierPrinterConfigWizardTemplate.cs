using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWizardTemplate : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("author")]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("command")]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("description")]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("source")]
        public partial long Source { get; set; }

        [ObservableProperty]
        [JsonPropertyName("uuid")]
        public partial Guid Uuid { get; set; }

        [ObservableProperty]
        [JsonPropertyName("version")]
        public partial long Version { get; set; }

        [ObservableProperty]
        [JsonPropertyName("visibleWhenPrinting")]
        public partial bool VisibleWhenPrinting { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigWizardTemplate);

        #endregion
    }
}

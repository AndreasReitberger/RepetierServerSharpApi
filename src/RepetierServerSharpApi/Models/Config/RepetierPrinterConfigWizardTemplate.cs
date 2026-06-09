using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWizardTemplate : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("author"), JsonPropertyName("author")]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("command"), JsonPropertyName("command")]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("description"), JsonPropertyName("description")]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("icon"), JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("source"), JsonPropertyName("source")]
        public partial string Source { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("uuid"), JsonPropertyName("uuid")]
        public partial Guid Uuid { get; set; }

        [ObservableProperty]
        [JsonProperty("version"), JsonPropertyName("version")]
        public partial long Version { get; set; }

        [ObservableProperty]
        [JsonProperty("visibleWhenPrinting"), JsonPropertyName("visibleWhenPrinting")]
        public partial bool VisibleWhenPrinting { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

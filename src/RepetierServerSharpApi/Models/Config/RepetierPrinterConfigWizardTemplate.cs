using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWizardTemplate : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("author")]
        public partial string Author { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("command")]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("description")]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("source")]
        public partial string Source { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("uuid")]
        public partial Guid Uuid { get; set; }

        [ObservableProperty]
        
        [JsonProperty("version")]
        public partial long Version { get; set; }

        [ObservableProperty]
        
        [JsonProperty("visibleWhenPrinting")]
        public partial bool VisibleWhenPrinting { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

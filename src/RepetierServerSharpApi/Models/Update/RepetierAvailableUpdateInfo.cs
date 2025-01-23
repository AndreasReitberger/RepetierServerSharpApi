using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierAvailableUpdateInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("availableBrandingVersion")]
        public partial long AvailableBrandingVersion { get; set; }

        [ObservableProperty]
        
        [JsonProperty("betaActive")]
        public partial bool BetaActive { get; set; }

        [ObservableProperty]
        
        [JsonProperty("branded")]
        public partial bool Branded { get; set; }

        [ObservableProperty]
        
        [JsonProperty("currentBrandingVersion")]
        public partial long CurrentBrandingVersion { get; set; }

        [ObservableProperty]
        
        [JsonProperty("currentVersion")]
        public partial string CurrentVersion { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("demo")]
        public partial bool Demo { get; set; }

        [ObservableProperty]
        
        [JsonProperty("downloadUrl")]
        public partial Uri? DownloadUrl { get; set; }

        [ObservableProperty]
        
        [JsonProperty("features")]
        public partial long Features { get; set; }

        [ObservableProperty]
        
        [JsonProperty("free")]
        public partial bool Free { get; set; }

        [ObservableProperty]
        
        [JsonProperty("ignoreVersion")]
        public partial string IgnoreVersion { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("installerType")]
        public partial long InstallerType { get; set; }

        [ObservableProperty]
        
        [JsonProperty("licensed")]
        public partial bool Licensed { get; set; }

        [ObservableProperty]
        
        [JsonProperty("message")]
        public partial string Message { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("messageHtml")]
        public partial string MessageHtml { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("printerFrontendUrl")]
        public partial string PrinterFrontendUrl { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("showUpdate")]
        public partial bool ShowUpdate { get; set; }

        [ObservableProperty]
        
        [JsonProperty("teaser")]
        public partial RepetierUpdateTeaser? Teaser { get; set; }

        [ObservableProperty]
        
        [JsonProperty("testperiodMode")]
        public partial long TestperiodMode { get; set; }

        [ObservableProperty]
        
        [JsonProperty("updateAvailable")]
        public partial bool UpdateAvailable { get; set; }

        [ObservableProperty]
        
        [JsonProperty("versionMessage")]
        public partial string VersionMessage { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("versionMessageHtml")]
        public partial string VersionMessageHtml { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("versionName")]
        public partial string VersionName { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("webFrontendUrl")]
        public partial string WebFrontendUrl { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

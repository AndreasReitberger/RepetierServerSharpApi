using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierAvailableUpdateInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("availableBrandingVersion")]
        public partial long AvailableBrandingVersion { get; set; }

        [ObservableProperty]
        [JsonPropertyName("betaActive")]
        public partial bool BetaActive { get; set; }

        [ObservableProperty]
        [JsonPropertyName("branded")]
        public partial bool Branded { get; set; }

        [ObservableProperty]
        [JsonPropertyName("currentBrandingVersion")]
        public partial long CurrentBrandingVersion { get; set; }

        [ObservableProperty]
        [JsonPropertyName("currentVersion")]
        public partial string CurrentVersion { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("demo")]
        public partial bool Demo { get; set; }

        [ObservableProperty]
        [JsonPropertyName("downloadUrl")]
        public partial Uri? DownloadUrl { get; set; }

        [ObservableProperty]
        [JsonPropertyName("features")]
        public partial long Features { get; set; }

        [ObservableProperty]
        [JsonPropertyName("free")]
        public partial bool Free { get; set; }

        [ObservableProperty]
        [JsonPropertyName("ignoreVersion")]
        public partial string IgnoreVersion { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("installerType")]
        public partial long InstallerType { get; set; }

        [ObservableProperty]
        [JsonPropertyName("licensed")]
        public partial bool Licensed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("message")]
        public partial string Message { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("messageHtml")]
        public partial string MessageHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("printerFrontendUrl")]
        public partial string PrinterFrontendUrl { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("showUpdate")]
        public partial bool ShowUpdate { get; set; }

        [ObservableProperty]
        [JsonPropertyName("teaser")]
        public partial RepetierUpdateTeaser? Teaser { get; set; }

        [ObservableProperty]
        [JsonPropertyName("testperiodMode")]
        public partial long TestperiodMode { get; set; }

        [ObservableProperty]
        [JsonPropertyName("updateAvailable")]
        public partial bool UpdateAvailable { get; set; }

        [ObservableProperty]
        [JsonPropertyName("versionMessage")]
        public partial string VersionMessage { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("versionMessageHtml")]
        public partial string VersionMessageHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("versionName")]
        public partial string VersionName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("webFrontendUrl")]
        public partial string WebFrontendUrl { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this, RepetierSourceGenerationContext.Default.RepetierAvailableUpdateInfo);
        #endregion
    }
}

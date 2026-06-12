using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierAvailableUpdateInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("availableBrandingVersion"), JsonPropertyName("availableBrandingVersion")]
        public partial long AvailableBrandingVersion { get; set; }

        [ObservableProperty]
        [JsonProperty("betaActive"), JsonPropertyName("betaActive")]
        public partial bool BetaActive { get; set; }

        [ObservableProperty]
        [JsonProperty("branded"), JsonPropertyName("branded")]
        public partial bool Branded { get; set; }

        [ObservableProperty]
        [JsonProperty("currentBrandingVersion"), JsonPropertyName("currentBrandingVersion")]
        public partial long CurrentBrandingVersion { get; set; }

        [ObservableProperty]
        [JsonProperty("currentVersion"), JsonPropertyName("currentVersion")]
        public partial string CurrentVersion { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("demo"), JsonPropertyName("demo")]
        public partial bool Demo { get; set; }

        [ObservableProperty]
        [JsonProperty("downloadUrl"), JsonPropertyName("downloadUrl")]
        public partial Uri? DownloadUrl { get; set; }

        [ObservableProperty]
        [JsonProperty("features"), JsonPropertyName("features")]
        public partial long Features { get; set; }

        [ObservableProperty]
        [JsonProperty("free"), JsonPropertyName("free")]
        public partial bool Free { get; set; }

        [ObservableProperty]
        [JsonProperty("ignoreVersion"), JsonPropertyName("ignoreVersion")]
        public partial string IgnoreVersion { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("installerType"), JsonPropertyName("installerType")]
        public partial long InstallerType { get; set; }

        [ObservableProperty]
        [JsonProperty("licensed"), JsonPropertyName("licensed")]
        public partial bool Licensed { get; set; }

        [ObservableProperty]
        [JsonProperty("message"), JsonPropertyName("message")]
        public partial string Message { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("messageHtml"), JsonPropertyName("messageHtml")]
        public partial string MessageHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("printerFrontendUrl"), JsonPropertyName("printerFrontendUrl")]
        public partial string PrinterFrontendUrl { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("showUpdate"), JsonPropertyName("showUpdate")]
        public partial bool ShowUpdate { get; set; }

        [ObservableProperty]
        [JsonProperty("teaser"), JsonPropertyName("teaser")]
        public partial RepetierUpdateTeaser? Teaser { get; set; }

        [ObservableProperty]
        [JsonProperty("testperiodMode"), JsonPropertyName("testperiodMode")]
        public partial long TestperiodMode { get; set; }

        [ObservableProperty]
        [JsonProperty("updateAvailable"), JsonPropertyName("updateAvailable")]
        public partial bool UpdateAvailable { get; set; }

        [ObservableProperty]
        [JsonProperty("versionMessage"), JsonPropertyName("versionMessage")]
        public partial string VersionMessage { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("versionMessageHtml"), JsonPropertyName("versionMessageHtml")]
        public partial string VersionMessageHtml { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("versionName"), JsonPropertyName("versionName")]
        public partial string VersionName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("webFrontendUrl"), JsonPropertyName("webFrontendUrl")]
        public partial string WebFrontendUrl { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

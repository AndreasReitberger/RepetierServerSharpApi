using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierAvailableUpdateInfo
    {
        #region Properties
        [JsonProperty("availableBrandingVersion")]
        public long AvailableBrandingVersion { get; set; }

        [JsonProperty("betaActive")]
        public bool BetaActive { get; set; }

        [JsonProperty("branded")]
        public bool Branded { get; set; }

        [JsonProperty("currentBrandingVersion")]
        public long CurrentBrandingVersion { get; set; }

        [JsonProperty("currentVersion")]
        public string CurrentVersion { get; set; }

        [JsonProperty("demo")]
        public bool Demo { get; set; }

        [JsonProperty("downloadUrl")]
        public Uri DownloadUrl { get; set; }

        [JsonProperty("features")]
        public long Features { get; set; }

        [JsonProperty("free")]
        public bool Free { get; set; }

        [JsonProperty("ignoreVersion")]
        public string IgnoreVersion { get; set; }

        [JsonProperty("installerType")]
        public long InstallerType { get; set; }

        [JsonProperty("licensed")]
        public bool Licensed { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("messageHtml")]
        public string MessageHtml { get; set; }

        [JsonProperty("printerFrontendUrl")]
        public string PrinterFrontendUrl { get; set; }

        [JsonProperty("showUpdate")]
        public bool ShowUpdate { get; set; }

        [JsonProperty("teaser")]
        public RepetierUpdateTeaser Teaser { get; set; }

        [JsonProperty("testperiodMode")]
        public long TestperiodMode { get; set; }

        [JsonProperty("updateAvailable")]
        public bool UpdateAvailable { get; set; }

        [JsonProperty("versionMessage")]
        public string VersionMessage { get; set; }

        [JsonProperty("versionMessageHtml")]
        public string VersionMessageHtml { get; set; }

        [JsonProperty("versionName")]
        public string VersionName { get; set; }

        [JsonProperty("webFrontendUrl")]
        public string WebFrontendUrl { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

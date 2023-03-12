using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierAvailableUpdateInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("availableBrandingVersion")]
        long availableBrandingVersion;

        [ObservableProperty]
        [JsonProperty("betaActive")]
        bool betaActive;

        [ObservableProperty]
        [JsonProperty("branded")]
        bool branded;

        [ObservableProperty]
        [JsonProperty("currentBrandingVersion")]
        long currentBrandingVersion;

        [ObservableProperty]
        [JsonProperty("currentVersion")]
        string currentVersion;

        [ObservableProperty]
        [JsonProperty("demo")]
        bool demo;

        [ObservableProperty]
        [JsonProperty("downloadUrl")]
        Uri downloadUrl;

        [ObservableProperty]
        [JsonProperty("features")]
        long features;

        [ObservableProperty]
        [JsonProperty("free")]
        bool free;

        [ObservableProperty]
        [JsonProperty("ignoreVersion")]
        string ignoreVersion;

        [ObservableProperty]
        [JsonProperty("installerType")]
        long installerType;

        [ObservableProperty]
        [JsonProperty("licensed")]
        bool licensed;

        [ObservableProperty]
        [JsonProperty("message")]
        string message;

        [ObservableProperty]
        [JsonProperty("messageHtml")]
        string messageHtml;

        [ObservableProperty]
        [JsonProperty("printerFrontendUrl")]
        string printerFrontendUrl;

        [ObservableProperty]
        [JsonProperty("showUpdate")]
        bool showUpdate;

        [ObservableProperty]
        [JsonProperty("teaser")]
        RepetierUpdateTeaser teaser;

        [ObservableProperty]
        [JsonProperty("testperiodMode")]
        long testperiodMode;

        [ObservableProperty]
        [JsonProperty("updateAvailable")]
        bool updateAvailable;

        [ObservableProperty]
        [JsonProperty("versionMessage")]
        string versionMessage;

        [ObservableProperty]
        [JsonProperty("versionMessageHtml")]
        string versionMessageHtml;

        [ObservableProperty]
        [JsonProperty("versionName")]
        string versionName;

        [ObservableProperty]
        [JsonProperty("webFrontendUrl")]
        string webFrontendUrl;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

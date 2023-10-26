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
        [property: JsonIgnore]
        long availableBrandingVersion;

        [ObservableProperty]
        [JsonProperty("betaActive")]
        [property: JsonIgnore]
        bool betaActive;

        [ObservableProperty]
        [JsonProperty("branded")]
        [property: JsonIgnore]
        bool branded;

        [ObservableProperty]
        [JsonProperty("currentBrandingVersion")]
        [property: JsonIgnore]
        long currentBrandingVersion;

        [ObservableProperty]
        [JsonProperty("currentVersion")]
        [property: JsonIgnore]
        string currentVersion;

        [ObservableProperty]
        [JsonProperty("demo")]
        [property: JsonIgnore]
        bool demo;

        [ObservableProperty]
        [JsonProperty("downloadUrl")]
        [property: JsonIgnore]
        Uri downloadUrl;

        [ObservableProperty]
        [JsonProperty("features")]
        [property: JsonIgnore]
        long features;

        [ObservableProperty]
        [JsonProperty("free")]
        [property: JsonIgnore]
        bool free;

        [ObservableProperty]
        [JsonProperty("ignoreVersion")]
        [property: JsonIgnore]
        string ignoreVersion;

        [ObservableProperty]
        [JsonProperty("installerType")]
        [property: JsonIgnore]
        long installerType;

        [ObservableProperty]
        [JsonProperty("licensed")]
        [property: JsonIgnore]
        bool licensed;

        [ObservableProperty]
        [JsonProperty("message")]
        [property: JsonIgnore]
        string message;

        [ObservableProperty]
        [JsonProperty("messageHtml")]
        [property: JsonIgnore]
        string messageHtml;

        [ObservableProperty]
        [JsonProperty("printerFrontendUrl")]
        [property: JsonIgnore]
        string printerFrontendUrl;

        [ObservableProperty]
        [JsonProperty("showUpdate")]
        [property: JsonIgnore]
        bool showUpdate;

        [ObservableProperty]
        [JsonProperty("teaser")]
        [property: JsonIgnore]
        RepetierUpdateTeaser teaser;

        [ObservableProperty]
        [JsonProperty("testperiodMode")]
        [property: JsonIgnore]
        long testperiodMode;

        [ObservableProperty]
        [JsonProperty("updateAvailable")]
        [property: JsonIgnore]
        bool updateAvailable;

        [ObservableProperty]
        [JsonProperty("versionMessage")]
        [property: JsonIgnore]
        string versionMessage;

        [ObservableProperty]
        [JsonProperty("versionMessageHtml")]
        [property: JsonIgnore]
        string versionMessageHtml;

        [ObservableProperty]
        [JsonProperty("versionName")]
        [property: JsonIgnore]
        string versionName;

        [ObservableProperty]
        [JsonProperty("webFrontendUrl")]
        [property: JsonIgnore]
        string webFrontendUrl;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

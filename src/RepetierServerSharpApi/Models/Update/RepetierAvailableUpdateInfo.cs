using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierAvailableUpdateInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("availableBrandingVersion")]      
        long availableBrandingVersion;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("betaActive")]      
        bool betaActive;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("branded")]       
        bool branded;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("currentBrandingVersion")]      
        long currentBrandingVersion;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("currentVersion")]
        string currentVersion = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("demo")]
        bool demo;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("downloadUrl")]      
        Uri? downloadUrl;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("features")]      
        long features;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("free")]  
        bool free;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ignoreVersion")]      
        string ignoreVersion = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("installerType")]    
        long installerType;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("licensed")]      
        bool licensed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("message")]     
        string message = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("messageHtml")]    
        string messageHtml = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printerFrontendUrl")]     
        string printerFrontendUrl = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("showUpdate")]     
        bool showUpdate;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("teaser")]      
        RepetierUpdateTeaser? teaser;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("testperiodMode")]       
        long testperiodMode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("updateAvailable")]       
        bool updateAvailable;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("versionMessage")]       
        string versionMessage = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("versionMessageHtml")]
        string versionMessageHtml = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("versionName")]       
        string versionName = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("webFrontendUrl")]       
        string webFrontendUrl = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

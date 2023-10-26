using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ExternalCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("confirm")]
        [property: JsonIgnore]
        string confirm;

        [ObservableProperty]
        [JsonProperty("execute")]
        [property: JsonIgnore]
        string execute;

        [ObservableProperty]
        [JsonProperty("icon")]
        [property: JsonIgnore]
        string icon;

        [ObservableProperty]
        [JsonProperty("id")]
        [property: JsonIgnore]
        long id;

        [ObservableProperty]
        [JsonProperty("ifAllNotPrinting")]
        [property: JsonIgnore]
        bool ifAllNotPrinting;

        [ObservableProperty]
        [JsonProperty("ifThisNotPrinting")]
        [property: JsonIgnore]
        bool ifThisNotPrinting;

        [ObservableProperty]
        [JsonProperty("local")]
        [property: JsonIgnore]
        bool local;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("permAdd")]
        [property: JsonIgnore]
        bool permAdd;

        [ObservableProperty]
        [JsonProperty("permConfig")]
        [property: JsonIgnore]
        bool permConfig;

        [ObservableProperty]
        [JsonProperty("permDel")]
        [property: JsonIgnore]
        bool permDel;

        [ObservableProperty]
        [JsonProperty("permPrint")]
        [property: JsonIgnore]
        bool permPrint;

        [ObservableProperty]
        [JsonProperty("remote")]
        [property: JsonIgnore]
        bool remote;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;

        [ObservableProperty]
        [JsonProperty("terminal")]
        [property: JsonIgnore]
        string terminal;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

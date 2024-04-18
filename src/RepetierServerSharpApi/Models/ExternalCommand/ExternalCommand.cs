using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ExternalCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("confirm")]
        string confirm;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("execute")]
        string execute;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("icon")]
        string icon;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("id")]
        long id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ifAllNotPrinting")]
        bool ifAllNotPrinting;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ifThisNotPrinting")]
        bool ifThisNotPrinting;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("local")]
        bool local;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("permAdd")]
        bool permAdd;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("permConfig")]
        bool permConfig;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("permDel")]
        bool permDel;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("permPrint")]
        bool permPrint;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("remote")]
        bool remote;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]
        string slug;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("terminal")]
        string terminal;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

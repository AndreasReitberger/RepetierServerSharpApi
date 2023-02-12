using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ExternalCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("confirm")]
        string confirm;

        [ObservableProperty]
        [JsonProperty("execute")]
        string execute;

        [ObservableProperty]
        [JsonProperty("icon")]
        string icon;

        [ObservableProperty]
        [JsonProperty("id")]
        long id;

        [ObservableProperty]
        [JsonProperty("ifAllNotPrinting")]
        bool ifAllNotPrinting;

        [ObservableProperty]
        [JsonProperty("ifThisNotPrinting")]
        bool ifThisNotPrinting;

        [ObservableProperty]
        [JsonProperty("local")]
        bool local;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("permAdd")]
        bool permAdd;

        [ObservableProperty]
        [JsonProperty("permConfig")]
        bool permConfig;

        [ObservableProperty]
        [JsonProperty("permDel")]
        bool permDel;

        [ObservableProperty]
        [JsonProperty("permPrint")]
        bool permPrint;

        [ObservableProperty]
        [JsonProperty("remote")]
        bool remote;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug;

        [ObservableProperty]
        [JsonProperty("terminal")]
        string terminal;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

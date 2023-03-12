using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierQuickGcodeCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("command")]
        string command;

        [ObservableProperty]
        [JsonProperty("icon")]
        string icon;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("visibleWhenPrinting")]
        bool visibleWhenPrinting;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

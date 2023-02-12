using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigButtonCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("command")]
        string command;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

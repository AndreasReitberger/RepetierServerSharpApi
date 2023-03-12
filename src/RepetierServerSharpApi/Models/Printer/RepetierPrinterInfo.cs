using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("active")]
        bool active;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("online")]
        long online;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("active")]
        bool active;

        [ObservableProperty]
        [JsonProperty("job")]
        string job;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("online")]
        long online;

        [ObservableProperty]
        [JsonProperty("pauseState")]
        long pauseState;

        [ObservableProperty]
        [JsonProperty("paused")]
        bool paused;

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

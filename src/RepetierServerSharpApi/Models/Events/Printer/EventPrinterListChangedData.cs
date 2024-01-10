using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("active")]
        [property: JsonIgnore]
        bool active;

        [ObservableProperty]
        [JsonProperty("job")]
        [property: JsonIgnore]
        string job;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("online")]
        [property: JsonIgnore]
        long online;

        [ObservableProperty]
        [JsonProperty("pauseState")]
        [property: JsonIgnore]
        long pauseState;

        [ObservableProperty]
        [JsonProperty("paused")]
        [property: JsonIgnore]
        bool paused;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("active")]

        bool active;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("job")]

        string job;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]

        string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("online")]

        long online;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pauseState")]

        long pauseState;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("paused")]

        bool paused;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]

        string slug;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

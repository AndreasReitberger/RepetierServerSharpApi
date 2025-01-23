using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]

        [JsonProperty("job")]
        public partial string Job { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("online")]
        public partial long Online { get; set; }

        [ObservableProperty]

        [JsonProperty("pauseState")]
        public partial long PauseState { get; set; }

        [ObservableProperty]

        [JsonProperty("paused")]
        public partial bool Paused { get; set; }

        [ObservableProperty]

        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

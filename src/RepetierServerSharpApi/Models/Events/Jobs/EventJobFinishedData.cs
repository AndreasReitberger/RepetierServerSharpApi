using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventJobFinishedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("duration")]
        long? duration;

        [ObservableProperty]
        [JsonProperty("end")]
        long? end;

        [ObservableProperty]
        [JsonProperty("lines")]
        long? lines;

        [ObservableProperty]
        [JsonProperty("start")]
        long? start;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

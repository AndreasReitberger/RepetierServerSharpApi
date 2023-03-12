using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTemp : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("data")]
        EventTempData data;

        [ObservableProperty]
        [JsonProperty("event")]
        string eventName;
        //string @event;

        [ObservableProperty]
        [JsonProperty("printer")]
        string printer;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTimer : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("data")]
        object data;

        [ObservableProperty]
        [JsonProperty("event")]
        string eventName;
        //string @event;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

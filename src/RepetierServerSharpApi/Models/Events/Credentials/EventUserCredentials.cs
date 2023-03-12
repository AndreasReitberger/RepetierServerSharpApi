using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentials : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("data")]
        EventUserCredentialsData data;

        [ObservableProperty]
        [JsonProperty("event")]
        string eventName;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventSession : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("callback_id")]
        long callbackId;

        [ObservableProperty]
        [JsonProperty("data")]
        object data;

        [ObservableProperty]
        [JsonProperty("session")]
        string session;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

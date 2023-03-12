using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierEventContainer : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("callback_id")]
        long callbackId;

        [ObservableProperty]
        [JsonProperty("data")]
        List<RepetierEventData> data = new();

        [ObservableProperty]
        [JsonProperty("eventList")]
        bool eventList;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

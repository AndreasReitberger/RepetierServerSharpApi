using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginRequiredResult : ObservableObject
    {
        #region Properties
        [JsonProperty("callback_id")]
        long? callbackId;

        [JsonProperty("data")]
        List<RepetierLoginRequiredResultData> data = new();

        [JsonProperty("eventList")]
        bool? eventList;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

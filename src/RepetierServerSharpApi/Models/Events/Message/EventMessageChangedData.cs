using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventMessageChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("date")]
        public DateTimeOffset? date;

        [ObservableProperty]
        [JsonProperty("id")]
        public long? id;

        [ObservableProperty]
        [JsonProperty("link")]
        public string link;

        [ObservableProperty]
        [JsonProperty("msg")]
        public string msg;

        [ObservableProperty]
        [JsonProperty("pause")]
        public bool? pause;

        [ObservableProperty]
        [JsonProperty("slug")]
        public string slug;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

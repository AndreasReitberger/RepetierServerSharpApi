using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierUpdateTeaser : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("available")]
        bool available;

        [ObservableProperty]
        [JsonProperty("end")]
        long end;

        [ObservableProperty]
        [JsonProperty("msg")]
        string msg;

        [ObservableProperty]
        [JsonProperty("start")]
        long start;

        [ObservableProperty]
        [JsonProperty("updated")]
        long updated;

        [ObservableProperty]
        [JsonProperty("url")]
        Uri url;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }

}

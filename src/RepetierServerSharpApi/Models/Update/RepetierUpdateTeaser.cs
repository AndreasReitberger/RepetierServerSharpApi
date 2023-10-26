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
        [property: JsonIgnore]
        bool available;

        [ObservableProperty]
        [JsonProperty("end")]
        [property: JsonIgnore]
        long end;

        [ObservableProperty]
        [JsonProperty("msg")]
        [property: JsonIgnore]
        string msg;

        [ObservableProperty]
        [JsonProperty("start")]
        [property: JsonIgnore]
        long start;

        [ObservableProperty]
        [JsonProperty("updated")]
        [property: JsonIgnore]
        long updated;

        [ObservableProperty]
        [JsonProperty("url")]
        [property: JsonIgnore]
        Uri url;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

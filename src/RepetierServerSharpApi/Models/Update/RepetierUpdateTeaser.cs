using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierUpdateTeaser : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("available")]
        
        bool available;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("end")]
        
        long end;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("msg")]
        
        string msg = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("start")]
        
        long start;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("updated")]
        
        long updated;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("url")]
        
        Uri? url;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

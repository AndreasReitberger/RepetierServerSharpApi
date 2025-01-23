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
        public partial bool Available { get; set; }

        [ObservableProperty]

        [JsonProperty("end")]
        public partial long End { get; set; }

        [ObservableProperty]

        [JsonProperty("msg")]
        public partial string Msg { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("start")]
        public partial long Start { get; set; }

        [ObservableProperty]

        [JsonProperty("updated")]
        public partial long Updated { get; set; }

        [ObservableProperty]

        [JsonProperty("url")]
        public partial Uri? Url { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

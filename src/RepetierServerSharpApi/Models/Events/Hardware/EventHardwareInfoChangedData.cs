using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventHardwareInfoChangedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]

        [JsonProperty("list")]
        public partial List<HardwareInfo> List { get; set; } = new();

        [ObservableProperty]

        [JsonProperty("maxUrgency")]
        public partial long? MaxUrgency { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

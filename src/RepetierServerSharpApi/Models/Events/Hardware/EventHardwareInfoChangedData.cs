using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventHardwareInfoChangedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("list")]
        [property: JsonIgnore]
        List<HardwareInfo> list = new();

        [ObservableProperty]
        [JsonProperty("maxUrgency")]
        [property: JsonIgnore]
        long? maxUrgency;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

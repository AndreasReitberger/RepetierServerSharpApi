using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventHardwareInfoChangedData : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("list")]
        List<HardwareInfo> list = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("maxUrgency")]
        long? maxUrgency;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

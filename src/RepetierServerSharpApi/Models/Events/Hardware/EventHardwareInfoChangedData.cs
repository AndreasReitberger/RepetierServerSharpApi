using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventHardwareInfoChangedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("list")]
        List<HardwareInfo> list = new();

        [ObservableProperty]
        [JsonProperty("maxUrgency")]
        long? maxUrgency;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

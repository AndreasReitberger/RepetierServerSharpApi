using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterFan : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("on")]
        bool on;

        [ObservableProperty]
        [JsonProperty("voltage")]
        long voltage;

        #region Json Ignore
        [JsonIgnore]
        public int Speed => Convert.ToInt32(Math.Round((double)(Voltage / 255m * 100m), 0));

        #endregion

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

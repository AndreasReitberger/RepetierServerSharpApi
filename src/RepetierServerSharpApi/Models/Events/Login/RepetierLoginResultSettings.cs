using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResultSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("gcodeGroup")]
        string gcodeGroup;

        [ObservableProperty]
        [JsonProperty("gcodeSortBy")]
        long? gcodeSortBy;

        [ObservableProperty]
        [JsonProperty("gcodeViewMode")]
        long? gcodeViewMode;

        [ObservableProperty]
        [JsonProperty("tempDiagActive")]
        long? tempDiagActive;

        [ObservableProperty]
        [JsonProperty("tempDiagAll")]
        long? tempDiagAll;

        [ObservableProperty]
        [JsonProperty("tempDiagBed")]
        long? tempDiagBed;

        [ObservableProperty]
        [JsonProperty("tempDiagChamber")]
        long? tempDiagChamber;

        [ObservableProperty]
        [JsonProperty("tempDiagMode")]
        long? tempDiagMode;

        [ObservableProperty]
        [JsonProperty("theme")]
        string theme;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

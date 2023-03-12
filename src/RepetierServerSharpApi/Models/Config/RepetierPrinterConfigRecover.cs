using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigRecover : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("delayBeforeReconnect")]
        long delayBeforeReconnect;

        [ObservableProperty]
        [JsonProperty("enabled")]
        bool enabled;

        [ObservableProperty]
        [JsonProperty("extraZOnFirmwareDetect")]
        long extraZOnFirmwareDetect;

        [ObservableProperty]
        [JsonProperty("firmwarePowerlossSignal")]
        string firmwarePowerlossSignal;

        [ObservableProperty]
        [JsonProperty("maxTimeForAutocontinue")]
        long maxTimeForAutocontinue;

        [ObservableProperty]
        [JsonProperty("procedure")]
        string procedure;

        [ObservableProperty]
        [JsonProperty("reactivateBedOnConnect")]
        bool reactivateBedOnConnect;

        [ObservableProperty]
        [JsonProperty("replayExtruderSwitches")]
        bool replayExtruderSwitches;

        [ObservableProperty]
        [JsonProperty("runOnConnect")]
        string runOnConnect;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

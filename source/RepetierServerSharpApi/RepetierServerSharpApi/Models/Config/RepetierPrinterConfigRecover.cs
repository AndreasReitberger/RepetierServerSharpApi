using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConfigRecover
    {
        #region Properties
        [JsonProperty("delayBeforeReconnect")]
        public long DelayBeforeReconnect { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("extraZOnFirmwareDetect")]
        public long ExtraZOnFirmwareDetect { get; set; }

        [JsonProperty("firmwarePowerlossSignal")]
        public string FirmwarePowerlossSignal { get; set; }

        [JsonProperty("maxTimeForAutocontinue")]
        public long MaxTimeForAutocontinue { get; set; }

        [JsonProperty("procedure")]
        public string Procedure { get; set; }

        [JsonProperty("reactivateBedOnConnect")]
        public bool ReactivateBedOnConnect { get; set; }

        [JsonProperty("replayExtruderSwitches")]
        public bool ReplayExtruderSwitches { get; set; }

        [JsonProperty("runOnConnect")]
        public string RunOnConnect { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

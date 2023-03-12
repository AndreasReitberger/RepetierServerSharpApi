using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("compressCommunication")]
        bool compressCommunication;

        [ObservableProperty]
        [JsonProperty("connectionMethod")]
        long connectionMethod;

        [ObservableProperty]
        [JsonProperty("continueAfterFastReconnect")]
        bool continueAfterFastReconnect;

        [ObservableProperty]
        [JsonProperty("ip")]
        RepetierPrinterConnectionIp ip;

        [ObservableProperty]
        [JsonProperty("lcdTimeMode")]
        long lcdTimeMode;

        [ObservableProperty]
        [JsonProperty("password")]
        string password;

        [ObservableProperty]
        [JsonProperty("pipe")]
        RepetierPrinterConnectionPipe pipe;

        [ObservableProperty]
        [JsonProperty("powerOffIdleMinutes")]
        long powerOffIdleMinutes;

        [ObservableProperty]
        [JsonProperty("powerOffMaxTemperature")]
        long powerOffMaxTemperature;

        [ObservableProperty]
        [JsonProperty("resetScript")]
        string resetScript;

        [ObservableProperty]
        [JsonProperty("serial")]
        RepetierPrinterConnectionSerial serial;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

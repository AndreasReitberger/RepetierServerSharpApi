using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("compressCommunication")]
        [property: JsonIgnore]
        bool compressCommunication;

        [ObservableProperty]
        [JsonProperty("connectionMethod")]
        [property: JsonIgnore]
        long connectionMethod;

        [ObservableProperty]
        [JsonProperty("continueAfterFastReconnect")]
        [property: JsonIgnore]
        bool continueAfterFastReconnect;

        [ObservableProperty]
        [JsonProperty("ip")]
        [property: JsonIgnore]
        RepetierPrinterConnectionIp ip;

        [ObservableProperty]
        [JsonProperty("lcdTimeMode")]
        [property: JsonIgnore]
        long lcdTimeMode;

        [ObservableProperty]
        [JsonProperty("password")]
        [property: JsonIgnore]
        string password;

        [ObservableProperty]
        [JsonProperty("pipe")]
        [property: JsonIgnore]
        RepetierPrinterConnectionPipe pipe;

        [ObservableProperty]
        [JsonProperty("powerOffIdleMinutes")]
        [property: JsonIgnore]
        long powerOffIdleMinutes;

        [ObservableProperty]
        [JsonProperty("powerOffMaxTemperature")]
        [property: JsonIgnore]
        long powerOffMaxTemperature;

        [ObservableProperty]
        [JsonProperty("resetScript")]
        [property: JsonIgnore]
        string resetScript;

        [ObservableProperty]
        [JsonProperty("serial")]
        [property: JsonIgnore]
        RepetierPrinterConnectionSerial serial;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

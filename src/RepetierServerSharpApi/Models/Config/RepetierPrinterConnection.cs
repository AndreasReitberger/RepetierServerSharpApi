using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnection : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("compressCommunication")]
        bool compressCommunication;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("connectionMethod")]
        long connectionMethod;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("continueAfterFastReconnect")]
        bool continueAfterFastReconnect;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ip")]
        RepetierPrinterConnectionIp? ip;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("lcdTimeMode")]
        long lcdTimeMode;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("password")]
        string password = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pipe")]
        RepetierPrinterConnectionPipe? pipe;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("powerOffIdleMinutes")]
        long powerOffIdleMinutes;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("powerOffMaxTemperature")]
        long powerOffMaxTemperature;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("resetScript")]
        string resetScript = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("serial")]
        RepetierPrinterConnectionSerial? serial;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

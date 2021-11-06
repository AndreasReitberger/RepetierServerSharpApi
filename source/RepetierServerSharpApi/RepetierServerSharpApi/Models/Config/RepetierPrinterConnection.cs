using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConnection
    {
        #region Properties
        [JsonProperty("connectionMethod")]
        public long ConnectionMethod { get; set; }

        [JsonProperty("ip")]
        public RepetierPrinterConnectionIp Ip { get; set; }

        [JsonProperty("lcdTimeMode")]
        public long LcdTimeMode { get; set; }

        [JsonProperty("pipe")]
        public RepetierPrinterConnectionPipe Pipe { get; set; }

        [JsonProperty("resetScript")]
        public string ResetScript { get; set; }

        [JsonProperty("serial")]
        public RepetierPrinterConnectionSerial Serial { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

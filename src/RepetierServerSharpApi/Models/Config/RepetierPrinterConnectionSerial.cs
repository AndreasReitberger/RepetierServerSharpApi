using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionSerial
    {
        #region Properties
        [JsonProperty("baudrate")]
        public long Baudrate { get; set; }

        [JsonProperty("communicationTimeout")]
        public long CommunicationTimeout { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("dtr")]
        public long Dtr { get; set; }

        [JsonProperty("inputBufferSize")]
        public long InputBufferSize { get; set; }

        [JsonProperty("malyanHack")]
        public bool MalyanHack { get; set; }

        [JsonProperty("pingPong")]
        public bool PingPong { get; set; }

        [JsonProperty("rts")]
        public long Rts { get; set; }

        [JsonProperty("visibleWithoutRunning")]
        public bool VisibleWithoutRunning { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

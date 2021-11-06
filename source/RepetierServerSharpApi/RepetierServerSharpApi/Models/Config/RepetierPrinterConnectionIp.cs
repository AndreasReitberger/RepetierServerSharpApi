using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConnectionIp
    {
        #region Properties
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("port")]
        public long Port { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }

}

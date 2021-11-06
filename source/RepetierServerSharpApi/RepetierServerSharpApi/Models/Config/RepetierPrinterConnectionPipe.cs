using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConnectionPipe
    {
        #region Properties
        [JsonProperty("file")]
        public string File { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

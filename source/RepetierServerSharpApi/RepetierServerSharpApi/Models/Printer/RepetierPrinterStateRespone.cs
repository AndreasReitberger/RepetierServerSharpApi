using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterStateRespone
    {
        #region Properties
        [JsonProperty("Printer")]
        public RepetierPrinterState Printer { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

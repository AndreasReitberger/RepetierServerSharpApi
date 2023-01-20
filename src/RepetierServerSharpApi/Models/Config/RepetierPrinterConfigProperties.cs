using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigProperties
    {
        #region Properties
        [JsonProperty("firmwareuploader_board")]
        public long FirmwareuploaderBoard { get; set; }

        [JsonProperty("firmwareuploader_extraPort")]
        public string FirmwareuploaderExtraPort { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

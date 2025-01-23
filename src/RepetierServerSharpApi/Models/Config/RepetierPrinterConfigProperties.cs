using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigProperties : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("firmwareuploader_board")]
        public partial long FirmwareuploaderBoard { get; set; }

        [ObservableProperty]
        
        [JsonProperty("firmwareuploader_extraPort")]
        public partial string FirmwareuploaderExtraPort { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

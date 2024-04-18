using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigProperties : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("firmwareuploader_board")]
        long firmwareuploaderBoard;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("firmwareuploader_extraPort")]
        string firmwareuploaderExtraPort = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

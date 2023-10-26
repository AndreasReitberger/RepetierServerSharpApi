using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigProperties : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("firmwareuploader_board")]
        [property: JsonIgnore]
        long firmwareuploaderBoard;

        [ObservableProperty]
        [JsonProperty("firmwareuploader_extraPort")]
        [property: JsonIgnore]
        string firmwareuploaderExtraPort;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigProperties : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("firmwareuploader_board")]
        long firmwareuploaderBoard;

        [ObservableProperty]
        [JsonProperty("firmwareuploader_extraPort")]
        string firmwareuploaderExtraPort;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

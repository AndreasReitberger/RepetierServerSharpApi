using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsFolderRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("folder")]
        RepetierProjectFolder folder;

        [ObservableProperty]
        [JsonProperty("ok")]
        bool ok;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

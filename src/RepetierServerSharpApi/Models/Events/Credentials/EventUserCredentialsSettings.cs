using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsSettings : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("gcodeGroup")]
        string gcodeGroup;

        [ObservableProperty]
        [JsonProperty("gcodeSortBy")]
        long gcodeSortBy;

        [ObservableProperty]
        [JsonProperty("gcodeViewMode")]
        long gcodeViewMode;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

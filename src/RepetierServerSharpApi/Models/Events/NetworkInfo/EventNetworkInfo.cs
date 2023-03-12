using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("data")]
        EventNetworkInfoData data;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

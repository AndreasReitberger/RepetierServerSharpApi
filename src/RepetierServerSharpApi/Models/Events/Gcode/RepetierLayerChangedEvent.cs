using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLayerChangedEvent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("layer")]
        long layer;

        [ObservableProperty]
        [JsonProperty("maxLayer")]
        long maxLayer;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

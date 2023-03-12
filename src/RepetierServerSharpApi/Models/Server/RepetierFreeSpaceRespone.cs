using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierFreeSpaceRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("available")]
        long available;

        [ObservableProperty]
        [JsonProperty("capacity")]
        long capacity;

        [ObservableProperty]
        [JsonProperty("free")]
        long free;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

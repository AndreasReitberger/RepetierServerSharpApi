using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventGcodeInfoUpdatedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("modelId")]
        long modelId;

        [ObservableProperty]
        [JsonProperty("modelPath")]
        string modelPath;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventGcodeInfoUpdatedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("list")]
        [property: JsonIgnore]
        string list;

        [ObservableProperty]
        [JsonProperty("modelId")]
        [property: JsonIgnore]
        long modelId;

        [ObservableProperty]
        [JsonProperty("modelPath")]
        [property: JsonIgnore]
        string modelPath;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventGcodeInfoUpdatedData : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("list")]
        string list;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("modelId")]
        long modelId;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("modelPath")]
        string modelPath;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]
        string slug;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

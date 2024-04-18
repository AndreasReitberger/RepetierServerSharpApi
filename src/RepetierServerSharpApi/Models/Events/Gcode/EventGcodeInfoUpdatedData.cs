using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventGcodeInfoUpdatedData : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("list")]
        string list = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("modelId")]
        long modelId;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("modelPath")]
        string modelPath = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]
        string slug = string.Empty;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

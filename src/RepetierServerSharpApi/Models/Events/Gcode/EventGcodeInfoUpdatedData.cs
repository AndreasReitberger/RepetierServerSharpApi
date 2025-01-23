using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventGcodeInfoUpdatedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("list")]
        public partial string List { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("modelId")]
        public partial long ModelId { get; set; }

        [ObservableProperty]
        
        [JsonProperty("modelPath")]
        public partial string ModelPath { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

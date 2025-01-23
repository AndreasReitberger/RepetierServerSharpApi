using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLayerChangedEvent : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("layer")]
        public partial long Layer { get; set; }

        [ObservableProperty]
        
        [JsonProperty("maxLayer")]
        public partial long MaxLayer { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("online")]
        public partial long Online { get; set; }

        [ObservableProperty]
        
        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

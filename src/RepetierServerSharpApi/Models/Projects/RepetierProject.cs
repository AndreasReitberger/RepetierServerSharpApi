using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProject : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("folder")]
        public partial long Folder { get; set; }

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("preview")]
        public partial string Preview { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("uuid")]
        public partial Guid Uuid { get; set; }

        [ObservableProperty]
        
        [JsonProperty("version")]
        public partial long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

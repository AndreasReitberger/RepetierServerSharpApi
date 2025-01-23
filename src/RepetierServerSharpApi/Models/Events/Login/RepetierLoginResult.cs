using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResult : ObservableObject//, IPrint3dLoginData
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("login")]
        public partial string Login { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("permissions")]
        public partial long? Permissions { get; set; }

        [ObservableProperty]
        
        [JsonProperty("serverUUID")]
        public partial Guid ServerUUID { get; set; }

        [ObservableProperty]
        
        [JsonProperty("settings")]
        public partial RepetierLoginResultSettings? Settings { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

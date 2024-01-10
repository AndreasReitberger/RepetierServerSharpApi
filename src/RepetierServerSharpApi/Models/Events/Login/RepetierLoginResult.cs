using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResult : ObservableObject//, IPrint3dLoginData
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("login")]
        [property: JsonIgnore]
        string login;

        [ObservableProperty]
        [JsonProperty("permissions")]
        [property: JsonIgnore]
        long? permissions;

        [ObservableProperty]
        [JsonProperty("serverUUID")]
        [property: JsonIgnore]
        Guid serverUUID;

        [ObservableProperty]
        [JsonProperty("settings")]
        [property: JsonIgnore]
        RepetierLoginResultSettings settings;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

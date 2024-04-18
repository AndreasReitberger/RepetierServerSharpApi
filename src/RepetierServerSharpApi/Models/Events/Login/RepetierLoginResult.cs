using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResult : ObservableObject//, IPrint3dLoginData
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("login")]
        string login;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("permissions")]
        long? permissions;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("serverUUID")]
        Guid serverUUID;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("settings")]
        RepetierLoginResultSettings settings;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

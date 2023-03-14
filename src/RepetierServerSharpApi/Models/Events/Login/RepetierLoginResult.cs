using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResult : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("login")]
        string login;

        [ObservableProperty]
        [JsonProperty("permissions")]
        long? permissions;

        [ObservableProperty]
        [JsonProperty("serverUUID")]
        Guid serverUUID;

        [ObservableProperty]
        [JsonProperty("settings")]
        RepetierLoginResultSettings settings;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

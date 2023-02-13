using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResult : ObservableObject
    {
        #region Properties
        [JsonProperty("login")]
        string login;

        [JsonProperty("permissions")]
        long? permissions;

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

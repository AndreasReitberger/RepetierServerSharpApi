using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("login")]
        string login;

        [ObservableProperty]
        [JsonProperty("permissions")]
        long permissions;

        [ObservableProperty]
        [JsonProperty("settings")]
        EventUserCredentialsSettings settings;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

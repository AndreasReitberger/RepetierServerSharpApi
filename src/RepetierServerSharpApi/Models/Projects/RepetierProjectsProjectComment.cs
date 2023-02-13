using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectComment : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("comment")]
        string comment;

        [ObservableProperty]
        [JsonProperty("time")]
        long? time;

        [ObservableProperty]
        [JsonProperty("user")]
        string user;
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

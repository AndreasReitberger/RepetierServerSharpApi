using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectFile : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("n")]
        string n;

        [ObservableProperty]
        [JsonProperty("s")]
        long? s;

        [ObservableProperty]
        [JsonProperty("p")]
        string p;
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

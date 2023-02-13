using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("ok")]
        bool? ok;

        [ObservableProperty]
        [JsonProperty("project")]
        RepetierProjectsProject project;
        #endregion 

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelGroup : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("groupNames")]
        string[] groupNames;

        [JsonProperty("ok")]
        [ObservableProperty]
        bool ok;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

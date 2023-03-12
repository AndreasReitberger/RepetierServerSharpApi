using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectSubFolder : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("empty")]
        bool empty;

        [ObservableProperty]
        [JsonProperty("idx")]
        long idx;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

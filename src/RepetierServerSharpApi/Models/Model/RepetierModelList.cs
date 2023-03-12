using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelList : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("data")]
        List<RepetierModel> data = new();

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

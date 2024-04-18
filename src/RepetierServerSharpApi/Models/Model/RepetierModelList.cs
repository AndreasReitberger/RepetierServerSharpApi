using Newtonsoft.Json;
using System.Collections.Generic;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelList : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("data")]
        List<RepetierModel> data = new();

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

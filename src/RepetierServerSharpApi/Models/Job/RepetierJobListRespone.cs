using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierJobListRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        List<RepetierJobListItem> data = new();

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

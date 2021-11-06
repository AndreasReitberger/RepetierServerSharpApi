using Newtonsoft.Json;
using System.Collections.Generic;


namespace AndreasReitberger.Models
{
    public partial class RepetierModelList
    {
        #region Properties
        [JsonProperty("data")]
        public List<RepetierModel> Data { get; set; } = new();

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

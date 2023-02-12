using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectParentElement
    {
        #region Properties
        [JsonProperty("empty")]
        public bool Empty { get; set; }

        [JsonProperty("idx")]
        public long Idx { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

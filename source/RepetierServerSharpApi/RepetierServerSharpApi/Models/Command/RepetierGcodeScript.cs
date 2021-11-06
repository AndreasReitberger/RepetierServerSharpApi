using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierGcodeScript
    {
        #region Properties
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }

}

using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterInfo
    {
        #region Properties
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("online")]
        public long Online { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

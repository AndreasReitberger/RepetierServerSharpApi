using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierMessage
    {
        #region Properties
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("pause")]
        public bool Pause { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object obj)
        {
            if (obj is not RepetierMessage item)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}

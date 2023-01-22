using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventPrinterListChangedData
    {
        #region Properties
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("online")]
        public long Online { get; set; }

        [JsonProperty("pauseState")]
        public long PauseState { get; set; }

        [JsonProperty("paused")]
        public bool Paused { get; set; }

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

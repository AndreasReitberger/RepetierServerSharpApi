using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierLoginRequiredResultData
    {
        #region Properties
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public RepetierLoginRequiredResultDataItem Data { get; set; }

        [JsonProperty("event", NullValueHandling = NullValueHandling.Ignore)]
        public string Event { get; set; }

        [JsonProperty("printer", NullValueHandling = NullValueHandling.Ignore)]
        public string Printer { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }

}

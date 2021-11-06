using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class EventGcodeInfoUpdatedData
    {
        #region Properties
        [JsonProperty("modelId")]
        public long ModelId { get; set; }

        [JsonProperty("modelPath")]
        public string ModelPath { get; set; }

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

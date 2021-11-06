using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierFreeSpaceRespone
    {
        #region Properties
        [JsonProperty("available")]
        public long Available { get; set; }

        [JsonProperty("capacity")]
        public long Capacity { get; set; }

        [JsonProperty("free")]
        public long Free { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

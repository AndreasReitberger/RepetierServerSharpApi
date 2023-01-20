using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTempData
    {
        #region Properties
        [JsonProperty("O")]
        public long O { get; set; }

        [JsonProperty("S")]
        public long S { get; set; }

        [JsonProperty("T")]
        public double T { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("t")]
        public long DataT { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        /*
        public override string ToString()
        {
            return $"S: {S}, T: {T}, O: {O} ({Id})";
        }*/
        #endregion
    }
}

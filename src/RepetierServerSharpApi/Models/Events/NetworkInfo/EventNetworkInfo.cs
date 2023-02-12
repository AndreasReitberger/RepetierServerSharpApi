using Newtonsoft.Json;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfo
    {
        #region Properties
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public EventNetworkInfoData Data { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

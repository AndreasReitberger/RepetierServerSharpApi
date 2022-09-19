using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventUserCredentialsSettings
    {
        #region Properties
        [JsonProperty("gcodeGroup")]
        public string GcodeGroup { get; set; }

        [JsonProperty("gcodeSortBy")]
        public long GcodeSortBy { get; set; }

        [JsonProperty("gcodeViewMode")]
        public long GcodeViewMode { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

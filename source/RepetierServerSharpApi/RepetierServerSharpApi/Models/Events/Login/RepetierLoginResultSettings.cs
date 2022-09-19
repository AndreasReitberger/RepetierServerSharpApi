using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResultSettings
    {
        #region Properties
        [JsonProperty("gcodeGroup", NullValueHandling = NullValueHandling.Ignore)]
        public string GcodeGroup { get; set; }

        [JsonProperty("gcodeSortBy", NullValueHandling = NullValueHandling.Ignore)]
        public long? GcodeSortBy { get; set; }

        [JsonProperty("gcodeViewMode", NullValueHandling = NullValueHandling.Ignore)]
        public long? GcodeViewMode { get; set; }

        [JsonProperty("tempDiagActive", NullValueHandling = NullValueHandling.Ignore)]
        public long? TempDiagActive { get; set; }

        [JsonProperty("tempDiagAll", NullValueHandling = NullValueHandling.Ignore)]
        public long? TempDiagAll { get; set; }

        [JsonProperty("tempDiagBed", NullValueHandling = NullValueHandling.Ignore)]
        public long? TempDiagBed { get; set; }

        [JsonProperty("tempDiagChamber", NullValueHandling = NullValueHandling.Ignore)]
        public long? TempDiagChamber { get; set; }

        [JsonProperty("tempDiagMode", NullValueHandling = NullValueHandling.Ignore)]
        public long? TempDiagMode { get; set; }

        [JsonProperty("theme", NullValueHandling = NullValueHandling.Ignore)]
        public string Theme { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

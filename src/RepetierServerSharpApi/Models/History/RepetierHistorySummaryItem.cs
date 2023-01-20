using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{

    public partial class RepetierHistorySummaryItem
    {
        #region Properties
        [JsonProperty("aborted")]
        public long Aborted { get; set; }

        [JsonProperty("computed")]
        public double Computed { get; set; }

        [JsonProperty("costs")]
        public double Costs { get; set; }

        [JsonProperty("filament")]
        public double Filament { get; set; }

        [JsonProperty("finished")]
        public long Finished { get; set; }

        [JsonProperty("month")]
        public long Month { get; set; }

        [JsonProperty("num")]
        public long Num { get; set; }

        [JsonProperty("real")]
        public double Real { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

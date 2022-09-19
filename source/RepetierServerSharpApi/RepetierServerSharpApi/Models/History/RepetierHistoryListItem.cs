using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistoryListItem
    {
        #region Properties
        [JsonProperty("computedTime")]
        public double ComputedTime { get; set; }

        [JsonProperty("costs")]
        public double Costs { get; set; }

        [JsonProperty("endTime")]
        public double EndTime { get; set; }

        [JsonProperty("filament")]
        public double Filament { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("month")]
        public long Month { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("pauseTime")]
        public double PauseTime { get; set; }

        [JsonProperty("printerName")]
        public string PrinterName { get; set; }

        [JsonProperty("printerSlug")]
        public string PrinterSlug { get; set; }

        [JsonProperty("printerUUID")]
        public string PrinterUuid { get; set; }

        [JsonProperty("report")]
        public string Report { get; set; }

        [JsonProperty("startTime")]
        public double StartTime { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

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

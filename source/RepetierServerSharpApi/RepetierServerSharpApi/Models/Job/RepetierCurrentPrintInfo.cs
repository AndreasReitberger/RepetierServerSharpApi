using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierCurrentPrintInfo
    {
        #region Properties
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("analysed")]
        public long Analysed { get; set; }

        [JsonProperty("done")]
        public double Done { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("jobid")]
        public long Jobid { get; set; }

        [JsonProperty("linesSend")]
        public long LinesSend { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ofLayer")]
        public long OfLayer { get; set; }

        [JsonProperty("online")]
        public long Online { get; set; }

        [JsonProperty("pauseState")]
        public long PauseState { get; set; }

        [JsonProperty("paused")]
        public bool Paused { get; set; }

        [JsonProperty("printStart")]
        public double PrintStart { get; set; }

        [JsonProperty("printTime")]
        public double PrintTime { get; set; }

        [JsonProperty("printedTimeComp")]
        public double PrintedTimeComp { get; set; }

        [JsonIgnore]
        public double RemainingPrintTime
        {
            get
            {
                if (PrintTime > 0)
                    return PrintTime - PrintedTimeComp;
                else
                    return 0;
            }
        }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("totalLines")]
        public long TotalLines { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

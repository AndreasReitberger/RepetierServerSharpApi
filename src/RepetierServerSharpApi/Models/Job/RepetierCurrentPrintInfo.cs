using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierCurrentPrintInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("active")]
        bool active;

        [ObservableProperty]
        [JsonProperty("analysed")]
        long analysed;

        [ObservableProperty]
        [JsonProperty("done")]
        double done;

        [ObservableProperty]
        [JsonProperty("job")]
        string job;

        [ObservableProperty]
        [JsonProperty("jobid")]
        long jobid;

        [ObservableProperty]
        [JsonProperty("jobstate")]
        string jobState;

        [ObservableProperty]
        [JsonProperty("linesSend")]
        long linesSend;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("ofLayer")]
        long ofLayer;

        [ObservableProperty]
        [JsonProperty("online")]
        long online;

        [ObservableProperty]
        [JsonProperty("pauseState")]
        long pauseState;

        [ObservableProperty]
        [JsonProperty("paused")]
        bool paused;

        [ObservableProperty]
        [JsonProperty("printStart")]
        double? printStart;

        [ObservableProperty]
        [JsonProperty("printTime")]
        double? printTime;

        [ObservableProperty]
        [JsonProperty("printedTimeComp")]
        double? printedTimeComp;

        [ObservableProperty]
        [JsonProperty("repeat")]
        long? repeat;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug;

        [ObservableProperty]
        [JsonProperty("start")]
        long? start;

        [ObservableProperty]
        [JsonProperty("totalLines")]
        long? totalLines;

        #region JsonIgnore


        [JsonIgnore]
        public double? RemainingPrintTime => PrintTime > 0 ? PrintTime - PrintedTimeComp : 0;

        #endregion

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

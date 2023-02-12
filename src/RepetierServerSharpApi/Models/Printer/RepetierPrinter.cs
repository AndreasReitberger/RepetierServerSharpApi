using AndreasReitberger.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinter : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("active")]
        bool active;

        [ObservableProperty]
        [JsonProperty("analysed")]
        int? analysed;

        [ObservableProperty]
        [JsonProperty("done")]
        double? done;

        [ObservableProperty]
        [JsonProperty("job")]
        string job = string.Empty;

        [ObservableProperty]
        [JsonProperty("jobid")]
        int jobId;

        [ObservableProperty]
        [JsonProperty("jobstate")]
        string? jobState = string.Empty;
        
        [ObservableProperty]
        [JsonProperty("linesSend")]
        long? linesSend;

        [ObservableProperty]
        [JsonProperty("name")]
        string name = string.Empty;
        
        [ObservableProperty]
        [JsonProperty("ofLayer")]
        long? ofLayer;

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
        int? repeat;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug = string.Empty;

        [ObservableProperty]
        [JsonProperty("start")]
        long? start;

        [ObservableProperty]
        [JsonProperty("totalLines")]
        long? totalLines;

        #region JsonIgnored

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? extruder1 = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? extruder2 = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? heatedBed = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? chamber = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double progress = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double remainingPrintTime = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        bool isPrinting = false;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        bool isPaused = false;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        bool isSelected = false;

        #endregion

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override bool Equals(object obj)
        {
            if (obj is not RepetierPrinter item)
                return false;
            return Slug.Equals(item.Slug);
        }

        public override int GetHashCode()
        {
            return Slug.GetHashCode();
        }
        #endregion
    }
}

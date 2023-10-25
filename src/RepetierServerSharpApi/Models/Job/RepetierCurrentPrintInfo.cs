using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierCurrentPrintInfo : ObservableObject, IPrint3dJobStatus
    {
        #region Properties
        [ObservableProperty]
        [property: JsonIgnore]
        Guid id;

        [ObservableProperty]
        [JsonProperty("active")]
        [property: JsonIgnore]
        bool active;

        [ObservableProperty]
        [JsonProperty("analysed")]
        [property: JsonIgnore]
        long analysed;

        [ObservableProperty]
        [JsonProperty("done")]
        [property: JsonIgnore]
        double? done;

        [ObservableProperty]
        [JsonProperty("job")]
        [property: JsonIgnore]
        string fileName;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(JobId))]
        [property: JsonProperty("jobid")]
        long jobIdLong;
        partial void OnJobIdLongChanged(long value)
        {
            JobId = value.ToString() ?? "";
        }

        [ObservableProperty, JsonIgnore]
        //[property: JsonIgnore]
        string jobId;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(State))]
        [property: JsonProperty("jobstate")]
        string jobState;
        partial void OnJobStateChanged(string value)
        {
            State = value == "running" ? Print3dJobState.InProgress : Print3dJobState.Completed;
        }

        [ObservableProperty]
        [JsonProperty("linesSend")]
        [property: JsonIgnore]
        long linesSend;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string printerName;

        [ObservableProperty]
        [JsonProperty("ofLayer")]
        [property: JsonIgnore]
        long ofLayer;

        [ObservableProperty]
        [JsonProperty("online")]
        [property: JsonIgnore]
        long online;

        [ObservableProperty]
        [JsonProperty("pauseState")]
        [property: JsonIgnore]
        long pauseState;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(State))]
        [property: JsonProperty("paused")]
        bool paused;
        partial void OnPausedChanged(bool value)
        {
            State = value ? Print3dJobState.Paused : Print3dJobState.InProgress;
        }

        [ObservableProperty]
        [JsonProperty("printStart")]
        [property: JsonIgnore]
        double? startTime;

        [ObservableProperty]
        [property: JsonIgnore]
        double? endTime;

        [ObservableProperty]
        [JsonProperty("printTime")]
        [property: JsonIgnore]
        double? printDuration;

        [ObservableProperty]
        [JsonProperty("printedTimeComp")]
        [property: JsonIgnore]
        double? printDurationTimeComp;

        [ObservableProperty]
        [property: JsonIgnore]
        double? totalPrintDuration;

        [ObservableProperty]
        [JsonProperty("repeat")]
        [property: JsonIgnore]
        long? repeat;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;

        [ObservableProperty]
        [JsonProperty("start")]
        [property: JsonIgnore]
        long? start;

        [ObservableProperty]
        [JsonProperty("totalLines")]
        [property: JsonIgnore]
        long? totalLines;

        [ObservableProperty]
        [property: JsonIgnore]
        double? filamentUsed;

        [ObservableProperty]
        [property: JsonIgnore]
        bool fileExists;

        [ObservableProperty]
        [property: JsonIgnore]
        Print3dJobState state;
        
        [ObservableProperty]
        [property: JsonIgnore]
        IGcodeMeta meta;
        
        #region JsonIgnore

        [JsonIgnore]
        public double? RemainingPrintTime => PrintDuration > 0 ? PrintDuration - PrintDurationTimeComp : 0;
        #endregion

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            // Ordinarily, we release unmanaged resources here;
            // but all are wrapped by safe handles.

            // Release disposable objects.
            if (disposing)
            {
                // Nothing to do here
            }
        }
        #endregion

        #region Clone

        public object Clone() => MemberwiseClone();

        #endregion

    }
}

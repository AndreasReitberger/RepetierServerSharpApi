using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

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

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(StartTimeGeneralized))]
        [property: JsonProperty("printStart")]
        double? startTime;
        partial void OnStartTimeChanged(double? value)
        {
            if(value is not null )
                StartTimeGeneralized = TimeBaseConvertHelper.FromDouble(value);
        }

        [ObservableProperty]
        [property: JsonIgnore]
        DateTime? startTimeGeneralized;

        [ObservableProperty]
        [property: JsonIgnore]
        double? endTime;
        partial void OnEndTimeChanged(double? value)
        {
            if (value is not null)
                EndTimeGeneralized = TimeBaseConvertHelper.FromDouble(value);
        }

        [ObservableProperty]
        [property: JsonIgnore]
        DateTime? endTimeGeneralized;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(PrintDurationGeneralized))]
        [property: JsonProperty("printTime")]
        double? printDuration;
        partial void OnPrintDurationChanged(double? value)
        {
            if (value is not null)
                PrintDurationGeneralized = TimeBaseConvertHelper.FromDoubleHours(value);
        }

        [ObservableProperty]
        [property: JsonIgnore]
        TimeSpan? printDurationGeneralized;

        [ObservableProperty]
        [JsonProperty("printedTimeComp")]
        [property: JsonIgnore]
        double? printDurationTimeComp;

        [ObservableProperty]
        [property: JsonIgnore]
        double? totalPrintDuration;
        partial void OnTotalPrintDurationChanged(double? value)
        {
            if (value is not null)
                TotalPrintDurationGeneralized = TimeBaseConvertHelper.FromDoubleHours(value);
        }

        [ObservableProperty]
        [property: JsonIgnore]
        TimeSpan? totalPrintDurationGeneralized;

        [ObservableProperty]
        [JsonProperty("repeat")]
        [property: JsonIgnore]
        long? repeat;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(StartTime))]
        [property: JsonProperty("start")]
        long? start;
        partial void OnStartChanged(long? value)
        {
            if(value is not null)
                StartTime = value;
        }

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

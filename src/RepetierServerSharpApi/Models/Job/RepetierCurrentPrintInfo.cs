using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierCurrentPrintInfo : ObservableObject, IPrint3dJobStatus
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        Guid id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("active")]
        bool active;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("analysed")]
        long analysed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("done")]
        double? done;
        partial void OnDoneChanged(double? value)
        {
            if (value is not null)
                DonePercentage = value / 100;
            else
                DonePercentage = 0;
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("job")]
        string fileName = string.Empty;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(JobId))]
        [property: JsonProperty("jobid")]
        long jobIdLong;
        partial void OnJobIdLongChanged(long value)
        {
            JobId = value.ToString() ?? "";
        }

        [ObservableProperty, JsonIgnore]
        string jobId = string.Empty;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(State))]
        [property: JsonProperty("jobstate")]
        string jobState = string.Empty;
        partial void OnJobStateChanged(string value)
        {
            State = value == "running" ? Print3dJobState.InProgress : Print3dJobState.Completed;
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("linesSend")]
        long linesSend;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string printerName = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ofLayer")]
        long ofLayer;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("online")]
        long online;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pauseState")]
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
            if (value is not null)
                StartTimeGeneralized = TimeBaseConvertHelper.FromUnixDate(value);
        }

        [ObservableProperty, JsonIgnore]
        DateTime? startTimeGeneralized;

        [ObservableProperty, JsonIgnore]
        double? endTime;
        partial void OnEndTimeChanged(double? value)
        {
            if (value is not null)
                EndTimeGeneralized = TimeBaseConvertHelper.FromUnixDate(value);
        }

        [ObservableProperty, JsonIgnore]
        DateTime? endTimeGeneralized;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(PrintDurationGeneralized))]
        //[property: JsonProperty("printTime")]
        [property: JsonProperty("printedTimeComp")]
        double? printDuration;
        partial void OnPrintDurationChanged(double? value)
        {
            if (value is not null)
                PrintDurationGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
            //RemainingPrintTime = value > 0 ? value - PrintDurationTimeComp : 0;
        }

        [ObservableProperty, JsonIgnore]
        TimeSpan? printDurationGeneralized;

        /*
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printedTimeComp")]
        double? printDurationTimeComp;
        partial void OnPrintDurationTimeCompChanged(double? value)
        {
            RemainingPrintTime = value > 0 ? TotalPrintDuration - value : 0;
            PrintDuration = PrintDurationTimeComp;
        }
        */

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(TotalPrintDurationGeneralized))]
        [property: JsonProperty("printTime")]
        double? totalPrintDuration;
        partial void OnTotalPrintDurationChanged(double? value)
        {
            if (value is not null)
                TotalPrintDurationGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
            RemainingPrintTime = value > 0 ? value - PrintDuration : 0;
        }

        [ObservableProperty, JsonIgnore]
        TimeSpan? totalPrintDurationGeneralized;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("repeat")]
        long? repeat;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]
        string slug = string.Empty;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(StartTime))]
        [property: JsonProperty("start")]
        long? start;
        partial void OnStartChanged(long? value)
        {
            if (value is not null)
                StartTime = value;
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("totalLines")]
        long? totalLines;

        [ObservableProperty, JsonIgnore]
        double? filamentUsed;

        [ObservableProperty, JsonIgnore]
        double? donePercentage;

        [ObservableProperty, JsonIgnore]
        bool fileExists;

        [ObservableProperty, JsonIgnore]
        Print3dJobState? state;

        [ObservableProperty, JsonIgnore]
        IGcodeMeta? meta;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(RemainingPrintTimeGeneralized))]
        double ? remainingPrintTime;
        partial void OnRemainingPrintTimeChanged(double? value)
        {
            if (value is not null)
                RemainingPrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty, JsonIgnore]
        TimeSpan? remainingPrintTimeGeneralized;

        #region JsonIgnore
        /*
        [JsonIgnore]
        public double? RemainingPrintTime => PrintDuration > 0 ? PrintDuration - PrintDurationTimeComp : 0;
        */
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

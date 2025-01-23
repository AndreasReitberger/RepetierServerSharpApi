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
        [ObservableProperty]

        public partial Guid Id { get; set; }

        [ObservableProperty]

        [JsonProperty("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]

        [JsonProperty("analysed")]
        public partial long Analysed { get; set; }

        [ObservableProperty]

        [JsonProperty("done")]
        public partial double? Done { get; set; }

        partial void OnDoneChanged(double? value)
        {
            if (value is not null)
                DonePercentage = value / 100;
            else
                DonePercentage = 0;
        }

        [ObservableProperty]

        [JsonProperty("job")]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(JobId))]
        [JsonProperty("jobid")]
        public partial long JobIdLong { get; set; }

        partial void OnJobIdLongChanged(long value)
        {
            JobId = value.ToString() ?? "";
        }

        [ObservableProperty]

        public partial string JobId { get; set; } = string.Empty;

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(State))]
        [JsonProperty("jobstate")]
        public partial string JobState { get; set; } = string.Empty;

        partial void OnJobStateChanged(string value)
        {
            State = value == "running" ? Print3dJobState.InProgress : Print3dJobState.Completed;
        }

        [ObservableProperty]

        [JsonProperty("linesSend")]
        public partial long LinesSend { get; set; }

        [ObservableProperty]

        [JsonProperty("name")]
        public partial string PrinterName { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("ofLayer")]
        public partial long OfLayer { get; set; }

        [ObservableProperty]

        [JsonProperty("online")]
        public partial long Online { get; set; }

        [ObservableProperty]

        [JsonProperty("pauseState")]
        public partial long PauseState { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(State))]
        [JsonProperty("paused")]
        public partial bool Paused { get; set; }

        partial void OnPausedChanged(bool value)
        {
            State = value ? Print3dJobState.Paused : Print3dJobState.InProgress;
        }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(StartTimeGeneralized))]
        [JsonProperty("printStart")]
        public partial double? StartTime { get; set; }

        partial void OnStartTimeChanged(double? value)
        {
            if (value is not null)
                StartTimeGeneralized = TimeBaseConvertHelper.FromUnixDate(value);
        }

        [ObservableProperty]

        public partial DateTime? StartTimeGeneralized { get; set; }

        [ObservableProperty]

        public partial double? EndTime { get; set; }

        partial void OnEndTimeChanged(double? value)
        {
            if (value is not null)
                EndTimeGeneralized = TimeBaseConvertHelper.FromUnixDate(value);
        }

        [ObservableProperty]

        public partial DateTime? EndTimeGeneralized { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintDurationGeneralized))]
        //[property: JsonProperty("printTime")]
        [JsonProperty("printedTimeComp")]
        public partial double? PrintDuration { get; set; }

        partial void OnPrintDurationChanged(double? value)
        {
            if (value is not null)
                PrintDurationGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
            //RemainingPrintTime = value > 0 ? value - PrintDurationTimeComp : 0;
        }

        [ObservableProperty]

        public partial TimeSpan? PrintDurationGeneralized { get; set; }

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

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(TotalPrintDurationGeneralized))]
        [JsonProperty("printTime")]
        public partial double? TotalPrintDuration { get; set; }

        partial void OnTotalPrintDurationChanged(double? value)
        {
            if (value is not null)
                TotalPrintDurationGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
            RemainingPrintTime = value > 0 ? value - PrintDuration : 0;
        }

        [ObservableProperty]

        public partial TimeSpan? TotalPrintDurationGeneralized { get; set; }

        [ObservableProperty]

        [JsonProperty("repeat")]
        public partial long? Repeat { get; set; }

        [ObservableProperty]

        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(StartTime))]
        [JsonProperty("start")]
        public partial long? Start { get; set; }

        partial void OnStartChanged(long? value)
        {
            if (value is not null)
                StartTime = value;
        }

        [ObservableProperty]

        [JsonProperty("totalLines")]
        public partial long? TotalLines { get; set; }

        [ObservableProperty]

        public partial double? FilamentUsed { get; set; }

        [ObservableProperty]

        public partial double? DonePercentage { get; set; }

        [ObservableProperty]

        public partial bool FileExists { get; set; }

        [ObservableProperty]

        public partial Print3dJobState? State { get; set; }

        [ObservableProperty]

        public partial IGcodeMeta? Meta { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(RemainingPrintTimeGeneralized))]
        public partial double? RemainingPrintTime { get; set; }

        partial void OnRemainingPrintTimeChanged(double? value)
        {
            if (value is not null)
                RemainingPrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]

        public partial TimeSpan? RemainingPrintTimeGeneralized { get; set; }

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
